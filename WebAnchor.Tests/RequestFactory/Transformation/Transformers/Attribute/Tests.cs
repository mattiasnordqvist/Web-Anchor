using System.Net.Http;

using NUnit.Framework;

namespace WebAnchor.Tests.ProofOfConcepts.CustomTransformers.ChangingQueryStringParameter
{
    [TestFixture]
    public class Tests : WebAnchorTest
    {
        [Test]
        public void UrlWithQueryParams_CustomParameterNameResolver()
        {
            TestTheRequestMessage<ICustomerApi>(api => api.GetCustomers2(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?p_filter=drunk", m.RequestUri.ToString());
            });
        }
    }
}
