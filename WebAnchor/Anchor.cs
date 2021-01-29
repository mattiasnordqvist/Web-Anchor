using System;
using System.Reflection;
using System.Threading.Tasks;

using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    public class Anchor : IDisposable
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

        public void Dispose()
        {
            if (_shouldDisposeHttpClient)
            {
                HttpClient.Dispose();
            }
        }

        public async Task<T> Intercept<T>(MethodInfo methodInfo, object[] parameters)
        {
            var request = HttpRequestBuilder.Create(methodInfo, parameters);
            var handler = HttpResponseParser.FindHandler(methodInfo);
            if (handler == null)
            {
                throw new WebAnchorException($"Return type of method {methodInfo.Name} in {methodInfo.DeclaringType.FullName} cannot be handled by any of the registered response handlers.");
            }

            var httpResponseMessage = await HttpClient.SendAsync(request, handler.HttpCompletionOptions);
            return await handler.HandleAsync<T>(httpResponseMessage, methodInfo);
        }
    }
}