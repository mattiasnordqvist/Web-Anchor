using System.Collections.Generic;
using System.Net.Http;

using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.RequestFactory.Transformation.Custom
{
    public class Tests : WebAnchorTest
    {
        [Fact]
        public void UrlWithQueryParams_AddExtraParameter()
        {
            TestTheRequest<ICustomerApi>(api => api.GetCustomers("test"), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer?filter=test&extra=3", m.RequestUri.ToString());
            },
            x => x.Request.ParameterListTransformers.Add(new AddExtraQueryParameterTransformer("extra", new object[] { 3 })));
        }

        [Fact]
        public void UrlWithQueryParams_AddExtraParameter_ThroughSettings()
        {
            TestTheRequest<ICustomerApi>(api => api.GetCustomers("test"), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer?filter=test&extra=3", m.RequestUri.ToString());
            },
            settings: new AddExtraParameterSettings());
        }

        [Fact]
        public void UrlWithQueryParams_AddAnExtraTransformer()
        {
            TestTheRequest<ICustomerApi>(api => api.MethodWithListParameter(new List<string> { "abc", "bcd", "cde" }),
                m =>
                {
                    Assert.Equal(HttpMethod.Get, m.Method);
                    Assert.Equal("api/customer?names=abc&names=bcd&names=cde", m.RequestUri.ToString());
                },
                x => x.Request.ParameterListTransformers.Add(new ReverseParameterListTransformers()));
        }
    }
}
