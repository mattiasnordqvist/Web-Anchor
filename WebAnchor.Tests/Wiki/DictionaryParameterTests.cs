using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;
using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.Wiki
{
    public class DictionaryParameterTests : WebAnchorTest
    {
        [BaseLocation("api/customer")]
        public interface ICustomerApi
        {
            [Get("")]
            Task<List<Customer>> GetCustomers(Dictionary<string, object> filter);

            [Get("")]
            Task<List<Customer>> GetCustomersWithOtherParams(string name, Dictionary<string, object> filter, int? age = null);
        }

        [Fact]
        public void TestDictionaryParameterExpansion()
        {
            var filter = new Dictionary<string, object>
            {
                { "name", "Adam" },
                { "age", "35" }
            };

            TestTheRequest<ICustomerApi>(
               api => api.GetCustomers(filter),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api/customer?filter.name=Adam&filter.age=35", assertMe.RequestUri?.ToString());
               });
        }

        [Fact]
        public void TestDictionaryParameterWithOtherParameters()
        {
            var filter = new Dictionary<string, object>
            {
                { "city", "Stockholm" },
                { "country", "Sweden" }
            };

            TestTheRequest<ICustomerApi>(
               api => api.GetCustomersWithOtherParams("John", filter, 30),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   var uri = assertMe.RequestUri?.ToString();
                   Assert.Contains("name=John", uri);
                   Assert.Contains("filter.city=Stockholm", uri);
                   Assert.Contains("filter.country=Sweden", uri);
                   Assert.Contains("age=30", uri);
               });
        }

        [Fact]
        public void TestEmptyDictionary()
        {
            var filter = new Dictionary<string, object>();

            TestTheRequest<ICustomerApi>(
               api => api.GetCustomers(filter),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api/customer", assertMe.RequestUri?.ToString());
               });
        }

        [Fact]
        public void TestDictionaryWithNullValues()
        {
            var filter = new Dictionary<string, object>
            {
                { "name", "Adam" },
                { "city", null },
                { "age", "35" }
            };

            TestTheRequest<ICustomerApi>(
               api => api.GetCustomers(filter),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   var uri = assertMe.RequestUri?.ToString();
                   Assert.Contains("filter.name=Adam", uri);
                   Assert.Contains("filter.age=35", uri);
                   // Null values should still be included but with empty value
                   Assert.Contains("filter.city=", uri);
               });
        }

        public class Customer
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
        }
    }
}