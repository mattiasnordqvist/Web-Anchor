using System;

using Castle.DynamicProxy;

using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    internal class Anchor : IInterceptor
    {
        private readonly bool _shouldDisposeHttpClient;

        public Anchor(IHttpClient httpClient, HttpRequestFactory httpRequestBuilder, HttpResponseHandlersList httpResponseParser, bool shouldDisposeHttpClient)
        {
            _shouldDisposeHttpClient = shouldDisposeHttpClient;
            HttpClient = httpClient;
            HttpRequestBuilder = httpRequestBuilder;
            HttpResponseParser = httpResponseParser;
        }

        public IHttpClient HttpClient { get; set; }
        public HttpRequestFactory HttpRequestBuilder { get; set; }
        public HttpResponseHandlersList HttpResponseParser { get; set; }

        public void Intercept(IInvocation invocation)
        {
            if (HttpRequestBuilder.IsHttpRequestInvocation(invocation))
            {
                var request = HttpRequestBuilder.Create(invocation);
                var handler = HttpResponseParser.FindHandler(invocation);
                if (handler == null)
                {
                    throw new WebAnchorException($"Return type of method {invocation.Method.Name} in {invocation.Method.DeclaringType.FullName} cannot be handled by any of the registered response handlers.");
                }

                var httpResponseMessageTask = HttpClient.SendAsync(request, handler.HttpCompletionOptions);
                handler.Handle(httpResponseMessageTask, invocation);
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