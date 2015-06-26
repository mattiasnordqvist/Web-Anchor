using Newtonsoft.Json;

using NUnit.Framework;

using WebAnchor.Tests.ProofOfConcepts.ParsingTheLocationHeader.Fixtures;

namespace WebAnchor.Tests.ProofOfConcepts.HeaderExtraction
{
    [TestFixture]
    public class Tests : IntegrationTest
    {
        [Test]
        public async void ParsingTheLocationHeaderFromResponseBodyViaCustomResponseParser()
        {
            var settings = new TestSettings().OverrideContentDeserializer(new HeaderEnabledContentDeserializer(new JsonSerializer()));
            var customerApi = Api.For<ParsingTheLocationHeader.Fixtures.ICustomerApi>(Host, settings);
            var result = await customerApi.CreateCustomer(new Customer { Id = 1, Name = "Mighty Gazelle" });
            Assert.AreEqual("Mighty Gazelle", result.Name);
            Assert.AreEqual("api/customer/1", result.Location);
            Assert.AreEqual(1, result.Id);
        }
    }
}
