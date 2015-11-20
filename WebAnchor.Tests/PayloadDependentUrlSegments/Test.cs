using System.Net.Http;

using NUnit.Framework;

using WebAnchor.Tests.ACollectionOfRandomTests.Fixtures;
using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.PayloadDependentUrlSegments
{
    [TestFixture]
    public class Test : WebAnchorTest
    {
        [Test]
        public void TestWithSuperNiceTypedApiWhereTypeChangesTheUrl()
        {
            TestTheRequest<IDynamicTypedApi<Customer>>(
                api => api.PostThis(new Customer { Id = 1, Name = "Mighty Gazelle" }),
                assertMe =>
                {
                    Assert.AreEqual(HttpMethod.Post, assertMe.Method);
                    Assert.AreEqual("/api/customer", assertMe.RequestUri.ToString());
                });
        }

        [Test]
        public void TestWithSuperNiceTypedApiWhereTypeChangesTheUrlImplementedInABetterWay()
        {
            TestTheRequest<IDynamicTypedApi2<Customer>>(
                api => api.PostThis(new Customer { Id = 1, Name = "Mighty Gazelle" }),
                assertMe =>
                {
                    Assert.AreEqual(HttpMethod.Post, assertMe.Method);
                    Assert.AreEqual("/api/customer", assertMe.RequestUri.ToString());
                });
        }

        [Test]
        public void TestGetterAlso()
        {
            TestTheRequest<IAnyResourceApi<Customer>>(
                api => api.Get(1),
                assertMe =>
                {
                    Assert.AreEqual(HttpMethod.Get, assertMe.Method);
                    Assert.AreEqual("/api/customer/1", assertMe.RequestUri.ToString());
                });
        }
    }
}
