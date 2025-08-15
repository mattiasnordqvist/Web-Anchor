using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;
using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.Integration
{
    public class DictionaryIntegrationTest : WebAnchorTest
    {
        [BaseLocation("api")]
        public interface ITestApi
        {
            [Get("customers")]
            Task<string> GetCustomers(Dictionary<string, object> filter);
        }

        [Fact]
        public void TestDictionaryExpansion()
        {
            var filter = new Dictionary<string, object>
            {
                { "name", "Adam" },
                { "age", "35" }
            };

            TestTheRequest<ITestApi>(
                api => api.GetCustomers(filter),
                request =>
                {
                    var uri = request.RequestUri?.ToString();
                    Assert.NotNull(uri);
                    Assert.Contains("filter.name=Adam", uri);
                    Assert.Contains("filter.age=35", uri);
                    Assert.Equal(HttpMethod.Get, request.Method);
                });
        }
    }
}