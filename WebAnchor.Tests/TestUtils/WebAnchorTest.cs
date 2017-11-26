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
        protected void TestTheRequest<T>(Action<T> apiCall, Action<HttpRequestMessage> assertHttpRequestMessage, Action<HttpRequestFactory> configure = null, Action<IEnumerable<Parameter>, RequestTransformContext> assertParametersAndContext = null, ApiSettings settings = null) where T : class
        {
            var api = new ProxyGenerator().CreateInterfaceProxyWithoutTarget<T>(new RequestTester(assertHttpRequestMessage, configure, assertParametersAndContext, settings));
            apiCall(api);
        }

        protected TReturn GetResponse<T, TReturn>(Func<T, TReturn> apiCall, HttpResponseMessage responseMessage, ApiSettings settings = null)
            where T : class
        {
            var api = new ProxyGenerator().CreateInterfaceProxyWithoutTarget<T>(new FakeResponseCreator(responseMessage, settings));
            return apiCall(api);
        }
    }
}