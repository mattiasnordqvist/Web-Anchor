using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebAnchor.Attributes.Content;
using WebAnchor.Attributes.URL;
using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.ACollectionOfRandomTests
{
    public class MetadataClassTests
    {
        public class WhenCallingGetCustomersAsync : WebAnchorTest
        {
            [Fact]
            public void MethodShouldBeGet()
            {
                TestTheRequest<ICustomerApi, CustomerApiMetadata>(api => api.GetCustomersAsync(), m =>
                {
                    Assert.Equal(HttpMethod.Get, m.Method);
                    Assert.Equal("api/customer", m.RequestUri.ToString());
                });
            }

            [Fact]
            public void RequestUriShouldBeAsExpected()
            {
                TestTheRequest<ICustomerApi, CustomerApiMetadata>(api => api.GetCustomersAsync(), m =>
                {
                    Assert.Equal("api/customer", m.RequestUri.ToString());
                });
            }
        }

        public class WhenCallingAddCustomersAsync : WebAnchorTest
        {
            [Fact]
            public void MethodShouldBePost()
            {
                TestTheRequest<ICustomerApi, CustomerApiMetadata>(api => api.AddCustomerAsync(null), m =>
                {
                    Assert.Equal(HttpMethod.Post, m.Method);
                });
            }

            [Fact]
            public void RequestUriShouldBeAsExpected()
            {
                TestTheRequest<ICustomerApi, CustomerApiMetadata>(api => api.AddCustomerAsync(null), m =>
                {
                    Assert.Equal("api/customer", m.RequestUri.ToString());
                });
            }

            [Fact]
            public void ContentAttributeShouldBeUsed()
            {
                var customer = new Customer
                {
                    Id = 123,
                    Name = "John Doe"
                };
                TestTheRequest<ICustomerApi, CustomerApiMetadata>(api => api.AddCustomerAsync(customer), async m =>
                {
                    var jsonData = await m.Content.ReadAsStringAsync();
                    var payloadCustomer = JsonConvert.DeserializeObject<Customer>(jsonData);
                    Assert.Equal(123, payloadCustomer.Id);
                    Assert.Equal("John Doe", payloadCustomer.Name);
                });
            }
        }

        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public interface ICustomerApi
        {
            Task<IEnumerable<Customer>> GetCustomersAsync();
            Task AddCustomerAsync(Customer customer);
        }

        [BaseLocation("api/customer")]
        public abstract class CustomerApiMetadata : ICustomerApi
        {
            [Get]
            public abstract Task<IEnumerable<Customer>> GetCustomersAsync();
            [Post]
            public abstract Task AddCustomerAsync([Content] Customer customer);
        }
    }
}
