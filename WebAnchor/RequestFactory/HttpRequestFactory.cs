using System;
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
    public class HttpRequestFactory : IHttpRequestFactory
    {
        public HttpRequestFactory(IList<IParameterListTransformer> transformers)
        {
            ParameterListTransformers = transformers ?? new List<IParameterListTransformer>();
            InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl = true;
            TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators = true;
        }

        public IList<IParameterListTransformer> ParameterListTransformers { get; set; }
        public Parameters ResolvedParameters { get; set; }
        public bool InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl { get; set; }

        /// <summary>
        /// Decides if path seperators ("/") in url segment parameters should be considered a part of the url AS PATH SEPERATORS or just as characters.
        /// If you use [BaseLocation({location})] and location is replaced by "api/v2" by some substitution, you probably want the "/" to seperate one path segment "api"
        /// from path segment "v2". If that is how you like it, leave this setting as it is (true). If you want the "/" to be encoded as "%2F" and not look like a path segments 
        /// seperator, set this setting to false.
        /// </summary>
        public bool TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators { get; set; }

        public virtual void ValidateApi(Type type)
        {
            foreach (var parameterListTransformer in ParameterListTransformers)
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
            var requestTransformContext = new RequestTransformContext(new ApiInvocation(invocation));
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
            var transformedParameters = ParameterListTransformers.Aggregate(parameters,
                (current, transformer) => transformer.TransformParameters(current, requestTransformContext)
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

            return ((baseAttribute != null ? baseAttribute.BaseUrl + (InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl ? "/" : string.Empty) : string.Empty) + methodAttribute.URL).CleanUpUrlString();
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
            var value = parameter.Value?.ToString() ?? parameter.SourceValue.ToString();
            return TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators
                ? string.Join("/", value.Split('/').Select(WebUtility.UrlEncode))
                : WebUtility.UrlEncode(value);
        }

        protected virtual string CreateUrlParameter(Parameter parameter)
        {
            var value = parameter.Value?.ToString() ?? parameter.SourceValue.ToString();
            return $"{parameter.Name}={WebUtility.UrlEncode(value)}";
        }
    }
}
