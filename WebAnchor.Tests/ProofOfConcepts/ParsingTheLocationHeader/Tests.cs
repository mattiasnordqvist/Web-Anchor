using Newtonsoft.Json;

using NUnit.Framework;
using WebAnchor.ResponseParser.ResponseHandlers;
using WebAnchor.Tests.ACollectionOfRandomTests.Fixtures;
using WebAnchor.Tests.ProofOfConcepts.ParsingTheLocationHeader.Fixtures;
using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.ProofOfConcepts.ParsingTheLocationHeader
{
    [TestFixture]
    public class Tests : IntegrationTest
    {
        [Test]
        public async void ParsingTheLocationHeaderFromResponseBodyViaCustomResponseParser()
        {
            var settings = new ApiSettings();
            var index = settings.ResponseHandlers.FindIndex(x => x is AsyncDeserializingResponseHandler);
            settings.ResponseHandlers[index] = new AsyncDeserializingResponseHandler(new HeaderEnabledContentDeserializer(new JsonSerializer()));
            var customerApi = Api.For<ICustomerApi>(Host, settings);
            var result = await customerApi.CreateCustomer(new Customer { Id = 1, Name = "Mighty Gazelle" });
            Assert.AreEqual("Mighty Gazelle", result.Name);
            Assert.AreEqual("api/customer/1", result.Location);
            Assert.AreEqual(1, result.Id);
        }
    }
}
