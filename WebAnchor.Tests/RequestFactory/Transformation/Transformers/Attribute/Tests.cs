using System.Net.Http;

using NUnit.Framework;

using WebAnchor.Tests.RequestFactory.Transformation.Transformers.Attribute.Fixtures;
using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Attribute
{
    [TestFixture]
    public class Tests : WebAnchorTest
    {
        [Test]
        public void UrlRouteSubstitution_AttributeDrivenParameterValueResolver()
        {
            TestTheRequest<ICustomerApiWithAttributedMethods>(api => api.GetCustomer_PrefixedQueryParamValue(8), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer/80", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_AttributeDrivenParameterNameResolver()
        {
            TestTheRequest<ICustomerApiWithAttributedMethods>(api => api.GetCustomers_PrefixedQueryParamName(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?p_filter=drunk", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_AttributeDrivenParameterValueResolver()
        {
            TestTheRequest<ICustomerApiWithAttributedMethods>(api => api.GetCustomers_ReversedQueryParamValue(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?filter=knurd", m.RequestUri.ToString());
            });
        }
    }
}
