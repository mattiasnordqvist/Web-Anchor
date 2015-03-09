using System.Net.Http;

using Castle.DynamicProxy;

using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    public class Anchor : IInterceptor
    {
        public Anchor(HttpClient httpClient, IHttpRequestFactory httpRequestBuilder, IHttpResponseParser httpResponseParser)
        {
            HttpClient = httpClient;
            HttpRequestBuilder = httpRequestBuilder;
            HttpResponseParser = httpResponseParser;
        }

        public HttpClient HttpClient { get; set; }
        public IHttpRequestFactory HttpRequestBuilder { get; set; }
        public IHttpResponseParser HttpResponseParser { get; set; }

        public void Intercept(IInvocation invocation)
        {
            var request = HttpRequestBuilder.Create(invocation);
            var httpResponseMessage = HttpClient.SendAsync(request);
            HttpResponseParser.Parse(httpResponseMessage, invocation);
        }
    }
}