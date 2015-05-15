using System;
using System.Net.Http;

using Castle.DynamicProxy;

using Newtonsoft.Json;

using WebAnchor.RequestFactory;

namespace WebAnchor.Tests
{
    public class InvocationTester : IInterceptor
    {
        private readonly Action<HttpRequestMessage> _assert;

        private readonly Action<HttpRequestFactory> _configure;

        public InvocationTester(Action<HttpRequestMessage> assert, Action<HttpRequestFactory> configure = null)
        {
            _assert = assert;
            _configure = configure ?? (a => { });
        }

        public void Intercept(IInvocation invocation)
        {
            var listTransformers = new ApiSettings().CreateParameterListTransformers();
            var factory = new HttpRequestFactory(new ContentSerializer(new JsonSerializer()), listTransformers);
            _configure(factory);
            var httpRequest = factory.Create(invocation);
            _assert(httpRequest);
        }
    }
}