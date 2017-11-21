using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using WebAnchor.ResponseParser.ResponseHandlers;
using WebAnchor.Tests.ACollectionOfRandomTests.Fixtures;
using WebAnchor.Tests.ProofOfConcepts.ParsingTheLocationHeader.Fixtures;
using WebAnchor.Tests.TestUtils;

using Xunit;

namespace WebAnchor.Tests.ProofOfConcepts.ParsingTheLocationHeader
{
    public class Tests : WebAnchorTest
    {
        [Fact]
        public async Task ParsingTheLocationHeaderFromResponseBodyViaCustomResponseParser()
        {
            var settings = new ApiSettings();
            var index = settings.ResponseHandlers.FindIndex(x => x is AsyncDeserializingResponseHandler);
            settings.ResponseHandlers[index] = new AsyncDeserializingResponseHandler(new HeaderEnabledContentDeserializer(new JsonSerializer()));

            var fakedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(@"{id: 1, name: ""Mighty Gazelle""}", Encoding.UTF8, "application/json"),
            };
            fakedResponse.Headers.Add("location", "api/customer/1");

            var result = await GetResponse<ICustomerApi, Task<CustomerWithLocation>>(
                x => x.CreateCustomer(new Customer { Id = 1, Name = "Mighty Gazelle" }),
                fakedResponse,
                settings);

            Assert.Equal("Mighty Gazelle", result.Name);
            Assert.Equal("api/customer/1", result.Location);
            Assert.Equal(1, result.Id);
        }
    }
}
