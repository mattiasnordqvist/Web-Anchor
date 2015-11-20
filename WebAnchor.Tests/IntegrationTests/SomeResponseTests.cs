using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using NUnit.Framework;

using WebAnchor.ResponseParser;
using WebAnchor.Tests.ACollectionOfRandomTests.Fixtures;
using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.IntegrationTests
{
    [TestFixture]
    public class SomeResponseTests : WebAnchorTest
    {
        [Test]
        public async Task CreatingASimpleGetRequest2()
        {
            var result = await GetResponse<ITestApi, Task<HttpResponseMessage>>(api => api.GetCustomer(9), new HttpResponseMessage(HttpStatusCode.OK));
            Assert.That(result.IsSuccessStatusCode);
        }

        [Test]
        public async Task ParsingAJsonResponse()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(@"{id: 9, name: ""Black Bull""}", Encoding.UTF8, "application/json")
            };

            var result = await GetResponse<ITestApi, Task<Customer>>(api => api.GetCustomer3(9), response);
            Assert.AreEqual("Black Bull", result.Name);
            Assert.AreEqual(9, result.Id);
        }

        [Test]
        public async Task RetrievingA404_WithTaskOFHttpResponseMessage()
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

           var result = await GetResponse<ITestApi, Task<HttpResponseMessage>>(api => api.Get404(), response);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Test]
        public async Task RetrievingA404_WithTaskOfT_ThrowsException()
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            await
                AssertEx.ThrowsAsync<ApiException>(
                    async () => await GetResponse<ITestApi, Task<Customer>>(api => api.Get404Customer(), response));
        }

        [Test]
        public async Task ExpectedDataButServerReturnsNothingInContent()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Hello World!") };

            await
                AssertEx.ThrowsAsync<JsonReaderException>(
                    async () => await GetResponse<ITestApi, Task<Customer>>(api => api.GetAnObject(), response));
        }
    }
}
