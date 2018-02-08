using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.Parameters;
using WebAnchor.Attributes.URL;
using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.Wiki
{
    public class AliasTests : WebAnchorTest
    {
        [BaseLocation("api/customer")]
        public interface ICustomerApi
        {
            [Get("")]
            Task<HttpResponseMessage> GetCustomer([Alias("order-by")]string orderBy);
        }

        [Fact]
        public void TestGetCustomerWithAlias()
        {
            TestTheRequest<ICustomerApi>(
               api => api.GetCustomer("name"),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api/customer?order-by=name", assertMe.RequestUri.ToString());
               });
        }
    }
}
