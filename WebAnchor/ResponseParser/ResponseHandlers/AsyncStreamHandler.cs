using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAnchor.ResponseParser.ResponseHandlers
{
    public class AsyncStreamHandler : IResponseHandler
    {
        public AsyncStreamHandler()
        {
        }

        public HttpCompletionOption HttpCompletionOptions => HttpCompletionOption.ResponseHeadersRead;

        public bool CanHandle(MethodInfo methodInfo)
        {
            return methodInfo.ReturnType == typeof(Task<Stream>);
        }

        public async Task<T> HandleAsync<T>(HttpResponseMessage httpResponseMessage, MethodInfo methodInfo)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return (httpResponseMessage.Content.ReadAsStreamAsync() switch
                {
                    Task<T> t => await t.ConfigureAwait(false),
                    _ => throw new Exception()
                });
            }
            else
            {
                throw new ApiException(httpResponseMessage);
            }

        }
    }
}