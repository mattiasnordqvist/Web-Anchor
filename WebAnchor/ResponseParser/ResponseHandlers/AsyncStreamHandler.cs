using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace WebAnchor.ResponseParser.ResponseHandlers
{
    public class AsyncStreamHandler : IResponseHandler
    {
        public AsyncStreamHandler()
        {
        }

        public bool CanHandle(Task<HttpResponseMessage> httpResponseMessage, IInvocation invocation)
        {
            return invocation.Method.ReturnType == typeof(Task<Stream>);
        }

        public void Handle(Task<HttpResponseMessage> task, IInvocation invocation)
        {
            invocation.ReturnValue = task.Then(httpResponseMessage =>
            {
                return httpResponseMessage.Content.ReadAsStreamAsync();
            });
        }
    }
}