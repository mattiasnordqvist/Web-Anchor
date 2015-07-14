using System;
using System.Collections.Generic;
using System.Net.Http;

using Castle.DynamicProxy;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Tests.TestUtils
{
    public class WebAnchorTest
    {
        protected void TestTheRequest<T>(Action<T> action, Action<HttpRequestMessage> assertHttpRequestMessage, Action<HttpRequestFactory> configure = null, Action<IEnumerable<Parameter>, ParameterTransformContext> assertParametersAndContext = null) where T : class
        {
            var api = new ProxyGenerator().CreateInterfaceProxyWithoutTarget<T>(new InvocationTester(assertHttpRequestMessage, configure, assertParametersAndContext));
            action(api);
        }
    }
}