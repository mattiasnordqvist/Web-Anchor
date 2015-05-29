using System.Net.Http;

using NUnit.Framework;

namespace WebAnchor.Tests.PayloadDependentUrlSegments
{
    [TestFixture]
    public class Test : WebAnchorTest
    {
        [Test]
        public void TestWithSuperNiceTypedApiWhereTypeChangesTheUrl()
        {
            TestTheRequestMessage<IDynamicTypedApi<Customer>>(
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
            TestTheRequestMessage<IDynamicTypedApi2<Customer>>(
                api => api.PostThis(new Customer { Id = 1, Name = "Mighty Gazelle" }),
                assertMe =>
                {
                    Assert.AreEqual(HttpMethod.Post, assertMe.Method);
                    Assert.AreEqual("/api/customer", assertMe.RequestUri.ToString());
                });
        }
    }
}
