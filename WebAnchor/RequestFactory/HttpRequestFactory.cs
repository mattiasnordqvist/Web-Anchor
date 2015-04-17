using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;

using Castle.Core.Internal;
using Castle.DynamicProxy;

using WebAnchor.RequestFactory.Resolvers;
using WebAnchor.RequestFactory.Transformers;

namespace WebAnchor.RequestFactory
{
    public class HttpRequestFactory : IHttpRequestFactory
    {
        public HttpRequestFactory(IContentSerializer contentSerializer)
        {
            ContentSerializer = contentSerializer;
            DefaultParameterResolvers = CreateDefaultResolvers() ?? new List<IParameterResolver>();
            DefaultParameterListTransformers = CreateDefaultTransformers() ?? new List<IParameterListTransformer>();
        }

        public IContentSerializer ContentSerializer { get; set; }
        public List<IParameterResolver> DefaultParameterResolvers { get; set; }
        public List<IParameterListTransformer> DefaultParameterListTransformers { get; set; }
        public Parameters ResolvedParameters { get; set; }

        public virtual HttpRequestMessage Create(IInvocation invocation)
        {
            var resolvedParameters = ResolveParameters(invocation);
            ResolvedParameters = new Parameters(
                resolvedParameters.Where(x => x.ParameterType == ParameterType.Route),
                resolvedParameters.Where(x => x.ParameterType == ParameterType.Query),
                resolvedParameters.FirstOrDefault(x => x.ParameterType == ParameterType.Payload));
             
            var resolvedUrl = ResolveUrlRoute(invocation);
            var resolvedMethod = ResolveHttpMethod(invocation);

            var request = new HttpRequestMessage(resolvedMethod, resolvedUrl);

            if (resolvedMethod == HttpMethod.Post || resolvedMethod == HttpMethod.Put)
            {
                request.Content = ResolveContent(invocation);
            }

            return request;
        }

        protected virtual List<Parameter> ResolveParameters(IInvocation invocation)
        {
            var methodInfo = invocation.Method;
            var url = methodInfo.GetCustomAttribute<HttpAttribute>().URL;

            var invocationParameters =
               methodInfo.GetParameters()
                   .Select((x, i) => new { Index = i, ParameterInfo = x })
                   .Where(x => invocation.GetArgumentValue(x.Index) != null)
                   .Select(x => new Parameter(x.ParameterInfo, invocation.GetArgumentValue(x.Index), ResolveParameterType(x.ParameterInfo, url)))
                   .ToList();

            var transformedParameters = DefaultParameterListTransformers.Aggregate(invocationParameters,
                (current, transformer) => transformer.TransformParameters(current)
                                                     .ToList());
            transformedParameters.ForEach(ResolveParameter);
            return transformedParameters;
        }

        protected virtual List<IParameterResolver> CreateDefaultResolvers()
        {
            return new List<IParameterResolver>
            {
                new DefaultParameterResolver(),
                new FormattableParameterResolver()
            };
        }

        protected virtual List<IParameterListTransformer> CreateDefaultTransformers()
        {
            return new List<IParameterListTransformer>
            {
                new ParameterOfListTransformer()
            };
        }

        protected virtual HttpContent ResolveContent(IInvocation invocation)
        {
            return ContentSerializer.Serialize(ResolvedParameters.PayLoad);
        }

        protected virtual string ResolveUrlRoute(IInvocation invocation)
        {
            var methodInfo = invocation.Method;
            var methodAttribute = methodInfo.GetCustomAttribute<HttpAttribute>();
            var baseAttribute = methodInfo.DeclaringType.GetCustomAttribute<BaseLocationAttribute>();

            var substitutedUrl = methodAttribute.URL.Replace(
                    ResolvedParameters.RouteParameters.ToDictionary(x => CreateRouteSegmentId(x.ParameterInfo), CreateRouteSegmentValue));
            var urlParams = CreateUrlParams(ResolvedParameters.QueryParameters);

            var resolvedUrl = (baseAttribute != null ? baseAttribute.BaseUrl : string.Empty) + substitutedUrl + urlParams;
            return resolvedUrl;
        }

        protected virtual HttpMethod ResolveHttpMethod(IInvocation invocation)
        {
            var methodInfo = invocation.Method;
            var methodAttribute = methodInfo.GetCustomAttribute<HttpAttribute>();
            var resolvedMethod = methodAttribute.Method;
            return resolvedMethod;
        }

        protected virtual void ResolveParameter(Parameter parameter)
        {
            var defaultResolvers = DefaultParameterResolvers.AsEnumerable();
            foreach (var resolver in defaultResolvers)
            {
                resolver.Resolve(parameter);
            }
            
            if(parameter.ParameterInfo != null)
            {
                var parameterResolver = parameter.ParameterInfo.GetResolverAttributesChain<ParameterResolverAttribute>();
                foreach (var resolver in parameterResolver)
                {
                    resolver.Resolve(parameter);
                }
            }
        }

        protected virtual string CreateRouteSegmentId(ParameterInfo parameter)
        {
            return "{" + parameter.Name + "}";
        }

        protected virtual string CreateRouteSegmentValue(Parameter parameter)
        {
            return WebUtility.UrlEncode(parameter.Value);
        }

        protected virtual string CreateUrlParams(IEnumerable<Parameter> parameters)
        {
            if (parameters.Any())
            {
                return "?" + string.Join("&", parameters.Select(CreateUrlParameter));
            }

            return string.Empty;
        }

        protected virtual string CreateUrlParameter(Parameter parameter)
        {
            return string.Format("{0}={1}", parameter.Name, WebUtility.UrlEncode(parameter.Value));
        }

        private ParameterType ResolveParameterType(ParameterInfo parameterInfo, string url)
        {
            return parameterInfo.HasAttribute<PayloadAttribute>()
                       ? ParameterType.Payload
                       : (url.Contains(CreateRouteSegmentId(parameterInfo))
                            ? ParameterType.Route
                            : ParameterType.Query);
        }
    }
}
