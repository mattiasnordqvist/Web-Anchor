using System.Net.Http;

using WebAnchor.Tests.RequestFactory.Transformation.Transformers.Attribute.Fixtures;
using WebAnchor.Tests.TestUtils;

using Xunit;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Attribute
{
    public class Tests : WebAnchorTest
    {
        [Fact]
        public void UrlRouteSubstitution_AttributeDrivenParameterValueResolver()
        {
            TestTheRequest<ICustomerApiWithAttributedMethods>(api => api.GetCustomer_PrefixedQueryParamValue(8), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer/80", m.RequestUri.ToString());
            });
        }

        [Fact]
        public void UrlWithQueryParams_AttributeDrivenParameterNameResolver()
        {
            TestTheRequest<ICustomerApiWithAttributedMethods>(api => api.GetCustomers_PrefixedQueryParamName(filter: "drunk"), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer?p_filter=drunk", m.RequestUri.ToString());
            });
        }

        [Fact]
        public void UrlWithQueryParams_AttributeDrivenParameterValueResolver()
        {
            TestTheRequest<ICustomerApiWithAttributedMethods>(api => api.GetCustomers_ReversedQueryParamValue(filter: "drunk"), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer?filter=knurd", m.RequestUri.ToString());
            });
        }
    }
}
