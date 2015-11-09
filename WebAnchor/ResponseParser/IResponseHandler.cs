using System.Net.Http;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace WebAnchor.ResponseParser
{
    public interface IResponseHandler
    {
        bool CanHandle(Task<HttpResponseMessage> httpResponseMessage, IInvocation invocation);

        void Handle(Task<HttpResponseMessage> httpResponseMessage, IInvocation invocation);
    }
}