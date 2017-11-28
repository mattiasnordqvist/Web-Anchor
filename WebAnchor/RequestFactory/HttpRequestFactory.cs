using System;
using System.Collections.Generic;
using System.Globalization;
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
            requestTransformContext.UrlTemplate = ResolveUrlTemplate(invocation);

            ResolvedParameters = ResolveParameters(requestTransformContext);

            var resolvedUrl = ResolveUrl(requestTransformContext.UrlTemplate);
            var resolvedMethod = ResolveHttpMethod(invocation);

            var request = new HttpRequestMessage(resolvedMethod, resolvedUrl);

            if (resolvedMethod == HttpMethod.Post || resolvedMethod == HttpMethod.Put)
            {
                request.Content = ResolveContent(requestTransformContext);
            }

            foreach (var headerParameter in ResolvedParameters.HeaderParameters)
            {
                var headerValue = headerParameter.Value as string;
                if (headerValue != null)
                {
                    request.Headers.Add(headerParameter.Name, headerValue);
                }
                else
                {
                    var headerValues = headerParameter.Value as IEnumerable<string>;
                    if (headerValues != null)
                    {
                        request.Headers.Add(headerParameter.Name, headerValues);
                    }
                    else
                    {
                        throw new InvalidOperationException("A header value can be of type string or IEnumerable<string>, not " + headerParameter.Value.GetType());
                    }
                }
            }

            return request;
        }

        protected virtual Parameters ResolveParameters(RequestTransformContext requestTransformContext)
        {
            var parameters = new List<Parameter>();
            var transformedParameters = requestTransformContext.Settings.Request.ParameterListTransformers.Aggregate(parameters,
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
                return requestTransformContext.ContentSerializer.Serialize(ResolvedParameters.Content);
            }

            return null;
        }

        protected virtual string ResolveUrlTemplate(IInvocation invocation)
        {
            var methodInfo = invocation.Method;
            var methodAttribute = methodInfo.GetCustomAttribute<HttpAttribute>();
            var baseAttribute = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttribute<BaseLocationAttribute>();

            return ((baseAttribute != null ? baseAttribute.BaseUrl + (_settings.Request.InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl ? "/" : string.Empty) : string.Empty) + methodAttribute.URL).CleanUpUrlString();
        }

        protected virtual string ResolveUrl(string urlTemplate)
        {
            urlTemplate = urlTemplate.Replace(ResolvedParameters.RouteParameters.ToDictionary(x => CreateRouteSegmentId(x.Name), CreateRouteSegmentValue));
            urlTemplate = AppendUrlParams(urlTemplate, ResolvedParameters.QueryParameters);
            return urlTemplate;
        }

        protected virtual string AppendUrlParams(string url, IEnumerable<Parameter> queryParameters)
        {
            var urlParams = ResolvedParameters.QueryParameters.Any()
                                ? (url.Contains("?") ? "&" : "?")
                                  + string.Join("&", ResolvedParameters.QueryParameters.Select(CreateUrlParameter))
                                : string.Empty;
            return url + urlParams;
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
            var value = FormatFormattable(parameter.Value ?? parameter.SourceValue);
            return _settings.Request.TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators
                ? string.Join("/", value.Split('/').Select(WebUtility.UrlEncode))
                : WebUtility.UrlEncode(value);
        }

        protected virtual string FormatFormattable(object value)
        {
            return _settings.Request.FormatFormattables && value is IFormattable ? ((IFormattable)value)
                   .ToString(null, CultureInfo.InvariantCulture) : value.ToString();
        }

        protected virtual string CreateUrlParameter(Parameter parameter)
        {
            var value = FormatFormattable(parameter.Value ?? parameter.SourceValue);
            return $"{parameter.Name}={WebUtility.UrlEncode(value)}";
        }
    }
}
