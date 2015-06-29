using System.Collections.Generic;
using System.Net.Http;

using NUnit.Framework;

using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.RequestFactory.Transformation.Custom
{
    [TestFixture]
    public class Tests : WebAnchorTest
    {
        [Test]
        public void UrlWithQueryParams_AddExtraParameter()
        {
            TestTheRequestMessage<ICustomerApi>(api => api.GetCustomers("test"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?filter=test&extra=3", m.RequestUri.ToString());
            },
            x => x.ParameterListTransformers.Add(new AddExtraParameterTransformer("extra", 3)));
        }

        [Test]
        public void UrlWithQueryParams_AddAnExtraTransformer()
        {
            TestTheRequestMessage<ICustomerApi>(api => api.MethodWithListParameter(new List<string> { "abc", "bcd", "cde" }),
                m =>
                {
                    Assert.AreEqual(HttpMethod.Get, m.Method);
                    Assert.AreEqual("api/customer?names=cde&names=bcd&names=abc", m.RequestUri.ToString());
                },
                x => x.ParameterListTransformers.Add(new ReverseParameterListTransformers()));
        }
    }
}
