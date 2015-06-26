using System.Net.Http;

using NUnit.Framework;

using WebAnchor.Tests.RequestFactory.Transformation.Transformers.Attribute.Fixtures;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Attribute
{
    [TestFixture]
    public class Tests : WebAnchorTest
    {
        [Test]
        public void UrlWithQueryParams_AttributeDrivenParameterNameResolver()
        {
            TestTheRequestMessage<ICustomerApiWithAttributedMethods>(api => api.GetCustomers_PrefixedQueryParamName(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?p_filter=drunk", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_AttributeDrivenParameterValueResolver()
        {
            TestTheRequestMessage<ICustomerApiWithAttributedMethods>(api => api.GetCustomers_ReversedQueryParamValue(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?filter=knurd", m.RequestUri.ToString());
            });
        }
    }
}
