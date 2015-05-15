using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

using Castle.Core.Internal;
using Castle.DynamicProxy;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.RequestFactory
{
    public class HttpRequestFactory : IHttpRequestFactory
    {
        public HttpRequestFactory(IContentSerializer contentSerializer, IList<IParameterListTransformer> transformers)
        {
            ContentSerializer = contentSerializer;
            ParameterListTransformers = transformers ?? new List<IParameterListTransformer>();
        }

        public IContentSerializer ContentSerializer { get; set; }
        public IList<IParameterListTransformer> ParameterListTransformers { get; set; }
        public Parameters ResolvedParameters { get; set; }
        
        public virtual void ValidateApi(Type type)
        {
            foreach (var method in type.GetMethods())
            {
                if (method.GetCustomAttribute<HttpAttribute>() == null)
                {
                    throw new WebAnchorException(string.Format("The method {0} in {1} must be have an {2}", method.Name, method.DeclaringType.FullName, typeof(HttpAttribute).FullName));
                }
            }
        }

        public virtual HttpRequestMessage Create(IInvocation invocation)
        {
            var resolvedParameters = ResolveParameters(invocation);
            ResolvedParameters = new Parameters(
                resolvedParameters.Where(x => x.ParameterType == ParameterType.Route),
                resolvedParameters.Where(x => x.ParameterType == ParameterType.Query),
                resolvedParameters.FirstOrDefault(x => x.ParameterType == ParameterType.Content));
             
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

            var transformedParameters = ParameterListTransformers.Aggregate(invocationParameters,
                (current, transformer) => transformer.TransformParameters(current, new ParameterTransformContext(methodInfo))
                                                     .ToList());

            return transformedParameters;
        }

        protected virtual HttpContent ResolveContent(IInvocation invocation)
        {
            return ContentSerializer.Serialize(ResolvedParameters.Content);
        }

        protected virtual string ResolveUrlRoute(IInvocation invocation)
        {
            var methodInfo = invocation.Method;
            var methodAttribute = methodInfo.GetCustomAttribute<HttpAttribute>();
            var baseAttribute = methodInfo.DeclaringType.GetCustomAttribute<BaseLocationAttribute>();

            var substitutedUrl = methodAttribute.URL.Replace(
                    ResolvedParameters.RouteParameters.ToDictionary(x => CreateRouteSegmentId(x.Name), CreateRouteSegmentValue));
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

        protected virtual string CreateRouteSegmentId(string name)
        {
            return "{" + name + "}";
        }

        protected virtual string CreateRouteSegmentValue(Parameter parameter)
        {
            var value = parameter.Value != null
                           ? parameter.Value.ToString()
                           : parameter.ParameterValue.ToString();
            return WebUtility.UrlEncode(value);
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
            var value = parameter.Value != null
                            ? parameter.Value.ToString()
                            : parameter.ParameterValue.ToString();
            return string.Format("{0}={1}", parameter.Name, WebUtility.UrlEncode(value));
        }

        private ParameterType ResolveParameterType(ParameterInfo parameterInfo, string url)
        {
            return parameterInfo.HasAttribute<ContentAttribute>()
                       ? ParameterType.Content
                       : (url.Contains(CreateRouteSegmentId(parameterInfo.Name))
                            ? ParameterType.Route
                            : ParameterType.Query);
        }
    }
}
