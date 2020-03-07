using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Castle.DynamicProxy;
using WebAnchor.Attributes.URL;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.RequestFactory
{
    public class HttpRequestFactory
    {
        private readonly IApiSettings _settings;

        public HttpRequestFactory(IApiSettings settings)
        {
            _settings = settings;
        }

        public Parameters ResolvedParameters { get; set; }

        public virtual void ValidateApi(Type type)
        {
            foreach (var parameterListTransformer in _settings.Request.ParameterListTransformers)
            {
                parameterListTransformer.ValidateApi(type);
            }

            foreach (var method in type.GetMethods())
            {
                if (method.GetCustomAttribute<HttpAttribute>() == null)
                {
                    throw new WebAnchorException($"The method {method.Name} in {method.DeclaringType.FullName} must be have an {typeof(HttpAttribute).FullName}");
                }
            }
        }

        public bool IsHttpRequestInvocation(IInvocation invocation)
        {
            return invocation.Method.IsDefined(typeof(HttpAttribute), false);
        }

        public virtual HttpRequestMessage Create(IInvocation invocation)
        {
            var requestTransformContext = new RequestTransformContext(new ApiInvocation(invocation), _settings);
            requestTransformContext.UrlTemplate = ResolveUrlTemplate(invocation, requestTransformContext);

            ResolvedParameters = ResolveParameters(requestTransformContext);

            var resolvedUrl = ResolveUrl(requestTransformContext.UrlTemplate, requestTransformContext);
            var resolvedHttpAttribute = ResolveHttpMethodAttribute(invocation);
            var resolvedMethod = resolvedHttpAttribute.Method;

            var request = new HttpRequestMessage(resolvedMethod, resolvedUrl);

            if (resolvedHttpAttribute.IncludeContentInRequest)
            {
                request.Content = ResolveContent(requestTransformContext);
            }

            foreach (var headerParameter in ResolvedParameters.HeaderParameters)
            {
                var headerValues = headerParameter.Values.Select(x => requestTransformContext.ParameterValueFormatter.Format(x, headerParameter)).ToList();
                request.Headers.Add(headerParameter.Name, headerValues);
            }

            return request;
        }

        protected virtual Parameters ResolveParameters(RequestTransformContext requestTransformContext)
        {
            var parameters = new List<Parameter>();
            var transformedParameters = requestTransformContext.ParameterListTransformers.Aggregate(parameters,
                (current, transformer) => transformer.Apply(current, requestTransformContext)
                                                     .ToList());

            return new Parameters(
                transformedParameters.Where(x => x.ParameterType == ParameterType.Route),
                transformedParameters.Where(x => x.ParameterType == ParameterType.Query),
                transformedParameters.Where(x => x.ParameterType == ParameterType.Header),
                transformedParameters.FirstOrDefault(x => x.ParameterType == ParameterType.Content));
        }

        protected virtual HttpContent ResolveContent(RequestTransformContext requestTransformContext)
        {
            if (ResolvedParameters.Content != null)
            {
                return requestTransformContext.ContentSerializer.Serialize(ResolvedParameters.Content.Values.First(), ResolvedParameters.Content);
            }

            return null;
        }

        protected virtual string ResolveUrlTemplate(IInvocation invocation, RequestTransformContext requestTransformContext)
        {
            var methodInfo = invocation.Method;
            var methodAttribute = methodInfo.GetCustomAttribute<HttpAttribute>();
            var baseAttribute = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttribute<BaseLocationAttribute>();

            return ((baseAttribute != null ? baseAttribute.BaseUrl + (requestTransformContext.InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl ? "/" : string.Empty) : string.Empty) + methodAttribute.URL).CleanUpUrlString();
        }

        protected virtual string ResolveUrl(string urlTemplate, RequestTransformContext requestTransformContext)
        {
            var resolvedUrl = urlTemplate.Replace(ResolvedParameters.RouteParameters.ToDictionary(x => CreateRouteSegmentId(x.Name), x => CreateRouteSegmentValue(x, requestTransformContext)));
            resolvedUrl = AppendUrlParams(resolvedUrl, ResolvedParameters.QueryParameters, requestTransformContext);
            foreach(var normalizer in requestTransformContext.UrlNormalizers)
            {
                resolvedUrl = normalizer.Normalize(resolvedUrl);
            }
            return resolvedUrl;
        }

        protected virtual string AppendUrlParams(string url, IEnumerable<Parameter> queryParameters, RequestTransformContext requestTransformContext)
        {
            var urlParams = ResolvedParameters.QueryParameters.Any()
                                ? (url.Contains("?") ? "&" : "?")
                                  + string.Join("&", ResolvedParameters.QueryParameters.SelectMany(x => CreateUrlParameter(x, requestTransformContext)))
                                : string.Empty;
            return url + urlParams;
        }

        protected virtual HttpAttribute ResolveHttpMethodAttribute(IInvocation invocation)
        {
            var methodInfo = invocation.Method;
            var methodAttribute = methodInfo.GetCustomAttribute<HttpAttribute>();
            return methodAttribute;
        }

        protected virtual string CreateRouteSegmentId(string name)
        {
            return "{" + name + "}";
        }

        protected virtual string CreateRouteSegmentValue(Parameter parameter, RequestTransformContext requestTransformContext)
        {
            var value = requestTransformContext.ParameterValueFormatter.Format(parameter.Values.First(), parameter);
            return requestTransformContext.EncodeUrlSegmentSeparatorsInUrlSegmentSubstitutions
                ? WebUtility.UrlEncode(value)
                : string.Join("/", value.Split('/').Select(WebUtility.UrlEncode));
        }

        protected virtual List<string> CreateUrlParameter(Parameter parameter, RequestTransformContext requestTransformContext)
        {
            List<Tuple<string, string>> nameValuePairs;
            List<string> values = new List<string>();
            foreach (var value in (IEnumerable)parameter.Values)
            {
                values.Add(WebUtility.UrlEncode(requestTransformContext.ParameterValueFormatter.Format(value, parameter)));
            }

            nameValuePairs = requestTransformContext.QueryParameterListStrategy.CreateNameValuePairs(parameter, values).ToList();
            return nameValuePairs.Select(x => $"{x.Item1}={x.Item2}").ToList();
        }
    }
}
