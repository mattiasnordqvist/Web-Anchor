using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using WebAnchor.ResponseParser;
using WebAnchor.Tests.ACollectionOfRandomTests.Fixtures;
using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.IntegrationTests
{
    public class SomeResponseTests : WebAnchorTest
    {
        [Fact]
        public async Task CreatingASimpleGetRequest2()
        {
            var result = await GetResponse<ITestApi, Task<HttpResponseMessage>>(api => api.GetCustomer(9), new HttpResponseMessage(HttpStatusCode.OK));
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task ParsingAJsonResponse()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(@"{id: 9, name: ""Black Bull""}", Encoding.UTF8, "application/json")
            };

            var result = await GetResponse<ITestApi, Task<Customer>>(api => api.GetCustomer3(9), response);
            Assert.Equal("Black Bull", result.Name);
            Assert.Equal(9, result.Id);
        }

        [Fact]
        public async Task RetrievingA404_WithTaskOFHttpResponseMessage()
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            var result = await GetResponse<ITestApi, Task<HttpResponseMessage>>(api => api.Get404(), response);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task RetrievingA404_WithTaskOfT_ThrowsException()
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            await
                AssertEx.ThrowsAsync<ApiException>(
                    async () => await GetResponse<ITestApi, Task<Customer>>(api => api.Get404Customer(), response));
        }

        [Fact]
        public async Task ExpectedDataButServerReturnsNothingInContent()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Hello World!") };
            await
                AssertEx.ThrowsAsync<Exception>(
                    async () => await GetResponse<ITestApi, Task<Customer>>(async api => await api.GetAnObject(), response));
        }

        [Fact]
        public async Task SimpleTaskResponse()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Hello World!") };
            var task = GetResponse<ITestApi, Task>(async api => await api.GetJustTask(), response);
            await task;
            Assert.True(task.IsCompleted);
        }

        [Fact]
        public async Task SimpleTaskResponseWithException()
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound) { };
            var task = GetResponse<ITestApi, Task>(async api => await api.GetJustTask(), response);
            await AssertEx.ThrowsAsync<ApiException>(async () => await task);
        }
    }
}
