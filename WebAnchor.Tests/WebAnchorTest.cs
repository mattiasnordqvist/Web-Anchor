using System;
using System.Net.Http;

using Castle.DynamicProxy;

using WebAnchor.RequestFactory;

namespace WebAnchor.Tests
{
    public class WebAnchorTest
    {
        protected void Test<T>(Action<T> action, Action<HttpRequestMessage> assert, Action<HttpRequestFactory> configure = null) where T : class
        {
            var api = new ProxyGenerator().CreateInterfaceProxyWithoutTarget<T>(new InvocationTester(assert, configure));
            action(api);
        }
    }
}