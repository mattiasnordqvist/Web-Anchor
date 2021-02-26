using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.Content;
using WebAnchor.Attributes.URL;
using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.RequestFactory.Content
{
    public class FormUrlEncodedContentTests : WebAnchorTest
    {
        [BaseLocation("location")]
        public interface IApi
        {
            [Post("resource")]
            Task<HttpResponseMessage> Post([Content, FormUrlEncoded] Payload data);

            [Post("resource")]
            Task<HttpResponseMessage> PostCustomized([Content, FormUrlEncoded] CustomizedPayload data);
        }

        [Fact]
        public void TestFormUrlEncodedSerialization()
        {
            TestTheRequest<IApi>(
                   api => api.Post(new Payload(1, "Test")),
                   request =>
                   {
                       var content = request.Content?.ReadAsStringAsync().Result;
                       Assert.Equal("Id=1&Name=Test", content);
                   });
        }

        [Fact]
        public void TestCustomizedFormUrlEncodedSerialization()
        {
            TestTheRequest<IApi>(
                   api => api.PostCustomized(new CustomizedPayload(1, "Test")),
                   request =>
                   {
                       var content = request.Content?.ReadAsStringAsync().Result;
                       Assert.Equal("Id=1&Name=Test&refresh_token=my-token", content);
                   });
        }

        public class Payload
        {
            public Payload(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class CustomizedPayload
        {
            public CustomizedPayload(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public int Id { get; set; }
            public string Name { get; set; }

            [FormUrlEncodedProperty("refresh_token")]
            public string RefreshToken => "my-token";
        }
    }
}
