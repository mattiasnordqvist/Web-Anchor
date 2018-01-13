using System.Net.Http;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace WebAnchor.ResponseParser
{
    public interface IResponseHandler
    {
        bool CanHandle(Task<HttpResponseMessage> httpResponseMessage, IInvocation invocation);

        /// <summary>
        /// Should set the invocation.ReturnValue to the expected result.
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <param name="invocation"></param>
        void Handle(Task<HttpResponseMessage> httpResponseMessage, IInvocation invocation);
    }
}