using System;

using Castle.DynamicProxy;

using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    internal class Anchor : IInterceptor
    {
        private readonly bool _shouldDisposeHttpClient;

        public Anchor(IHttpClient httpClient, HttpRequestFactory httpRequestBuilder, HttpResponseParser httpResponseParser, bool shouldDisposeHttpClient)
        {
            _shouldDisposeHttpClient = shouldDisposeHttpClient;
            HttpClient = httpClient;
            HttpRequestBuilder = httpRequestBuilder;
            HttpResponseParser = httpResponseParser;
        }

        public IHttpClient HttpClient { get; set; }
        public HttpRequestFactory HttpRequestBuilder { get; set; }
        public HttpResponseParser HttpResponseParser { get; set; }

        public void Intercept(IInvocation invocation)
        {
            if (HttpRequestBuilder.IsHttpRequestInvocation(invocation))
            {
                var request = HttpRequestBuilder.Create(invocation);
                var httpResponseMessageTask = HttpClient.SendAsync(request, System.Net.Http.HttpCompletionOption.ResponseHeadersRead);
                HttpResponseParser.Parse(httpResponseMessageTask, invocation);
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