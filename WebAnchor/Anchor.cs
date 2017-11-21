using System;

using Castle.DynamicProxy;

using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    internal class Anchor : IInterceptor
    {
        private readonly bool _shouldDisposeHttpClient;

        public Anchor(IHttpClient httpClient, IHttpRequestFactory httpRequestBuilder, IHttpResponseParser httpResponseParser, bool shouldDisposeHttpClient)
        {
            _shouldDisposeHttpClient = shouldDisposeHttpClient;
            HttpClient = httpClient;
            HttpRequestBuilder = httpRequestBuilder;
            HttpResponseParser = httpResponseParser;
        }

        public IHttpClient HttpClient { get; set; }
        public IHttpRequestFactory HttpRequestBuilder { get; set; }
        public IHttpResponseParser HttpResponseParser { get; set; }

        public void Intercept(IInvocation invocation)
        {
            if (HttpRequestBuilder.IsHttpRequestInvocation(invocation))
            {
                var request = HttpRequestBuilder.Create(invocation);
                var httpResponseMessage = HttpClient.SendAsync(request);
                HttpResponseParser.Parse(httpResponseMessage, invocation);
            }
            else
            {
                if (invocation.Method.GetBaseDefinition().DeclaringType == typeof(IDisposable))
                {
                    if (_shouldDisposeHttpClient)
                    {
                        HttpClient.Dispose();
                    }
                }
            }
        }
    }
}