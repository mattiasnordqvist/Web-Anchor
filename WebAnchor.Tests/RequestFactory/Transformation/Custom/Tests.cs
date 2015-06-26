using System.Net.Http;

using NUnit.Framework;

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
    }
}
