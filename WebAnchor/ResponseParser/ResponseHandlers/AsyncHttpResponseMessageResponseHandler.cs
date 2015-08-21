using System.Net.Http;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace WebAnchor.ResponseParser.ResponseHandlers
{
    public class AsyncHttpResponseMessageResponseHandler : IResponseHandler
    {
        public bool CanHandle(Task<HttpResponseMessage> httpResponseMessage, IInvocation invocation)
        {
            return invocation.Method.ReturnType == typeof(Task<HttpResponseMessage>);
        }

        public object Handle(Task<HttpResponseMessage> httpResponseMessage, IInvocation invocation)
        {
            return httpResponseMessage;
        }
    }
}