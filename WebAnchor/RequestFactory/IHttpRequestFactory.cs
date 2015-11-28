using System;
using System.Net.Http;

using Castle.DynamicProxy;

namespace WebAnchor.RequestFactory
{
    public interface IHttpRequestFactory
    {
        HttpRequestMessage Create(IInvocation invocation);

        void ValidateApi(Type type);

        bool IsHttpRequestInvocation(IInvocation invocation);
    }
}
