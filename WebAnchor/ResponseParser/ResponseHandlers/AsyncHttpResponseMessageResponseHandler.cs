using System.Net.Http;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace WebAnchor.ResponseParser.ResponseHandlers
{
    public class AsyncHttpResponseMessageResponseHandler : IResponseHandler
    {
        public HttpCompletionOption HttpCompletionOptions => HttpCompletionOption.ResponseContentRead;

        public bool CanHandle(IInvocation invocation)
        {
            return invocation.Method.ReturnType == typeof(Task<HttpResponseMessage>);
        }

        public void Handle(Task<HttpResponseMessage> httpResponseMessage, IInvocation invocation)
        {
            invocation.ReturnValue = httpResponseMessage;
        }
    }
}