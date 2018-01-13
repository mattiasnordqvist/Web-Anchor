using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace WebAnchor.ResponseParser.ResponseHandlers
{
    public class AsyncStreamHandler : IResponseHandler
    {
        public AsyncStreamHandler()
        {
        }

        public HttpCompletionOption HttpCompletionOptions => HttpCompletionOption.ResponseHeadersRead;

        public bool CanHandle(IInvocation invocation)
        {
            return invocation.Method.ReturnType == typeof(Task<Stream>);
        }

        public void Handle(Task<HttpResponseMessage> task, IInvocation invocation)
        {
            invocation.ReturnValue = task.Then(httpResponseMessage =>
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return httpResponseMessage.Content.ReadAsStreamAsync();
                }
                else
                {
                    throw new ApiException(httpResponseMessage);
                }
            });
        }
    }
}