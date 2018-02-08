using System.Net.Http;

using WebAnchor.Tests.ACollectionOfRandomTests.Fixtures;
using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.PayloadDependentUrlSegments
{
    public class Test : WebAnchorTest
    {
        [Fact]
        public void TestWithSuperNiceTypedApiWhereTypeChangesTheUrlImplementedInABetterWay()
        {
            TestTheRequest<IDynamicTypedApi2<Customer>>(
                api => api.PostThis(new Customer { Id = 1, Name = "Mighty Gazelle" }),
                assertMe =>
                {
                    Assert.Equal(HttpMethod.Post, assertMe.Method);
                    Assert.Equal("/api/customer", assertMe.RequestUri.ToString());
                });
        }

        [Fact]
        public void TestGetterAlso()
        {
            TestTheRequest<IAnyResourceApi<Customer>>(
                api => api.Get(1),
                assertMe =>
                {
                    Assert.Equal(HttpMethod.Get, assertMe.Method);
                    Assert.Equal("/api/customer/1", assertMe.RequestUri.ToString());
                });
        }
    }
}
