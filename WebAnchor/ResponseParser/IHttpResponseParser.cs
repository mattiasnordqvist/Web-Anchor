using System;
using System.Net.Http;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace WebAnchor.ResponseParser
{
    public interface IHttpResponseParser
    {
        void Parse(Task<HttpResponseMessage> httpResponseMessage, IInvocation invocation);

        void ValidateApi(Type type);
    }
}
