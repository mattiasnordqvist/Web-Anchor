using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAnchor.ResponseParser.ResponseHandlers
{
    public class AsyncHttpResponseMessageResponseHandler : IResponseHandler
    {
        public HttpCompletionOption HttpCompletionOptions => HttpCompletionOption.ResponseContentRead;

        public bool CanHandle(MethodInfo methodInfo)
        {
            return methodInfo.ReturnType == typeof(Task<HttpResponseMessage>);
        }

        public Task<T> HandleAsync<T>(HttpResponseMessage httpResponseMessage, MethodInfo methodInfo)
        {
            return Task.FromResult(httpResponseMessage) switch
            {
                Task<T> t => t,
                _ => throw new Exception()
            };
        }
    }
}