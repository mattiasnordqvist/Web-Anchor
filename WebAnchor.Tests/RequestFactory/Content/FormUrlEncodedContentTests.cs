using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebAnchor.Attributes.Content;
using WebAnchor.Attributes.URL;
using WebAnchor.RequestFactory.Serialization;
using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.RequestFactory.Content
{
    public class XmlContentTests : WebAnchorTest
    {
        [BaseLocation("location")]
        public interface IApi
        {
            [Post("resource")]
            Task<HttpResponseMessage> Post([Content, Xml] Payload data);

            [Post("resource")]
            Task<HttpResponseMessage> PostCustomized([Content, Xml] CustomizedPayload data);
        }

        [Fact]
        public void TestXmlSerialization()
        {
            TestTheRequest<IApi>(
                   api => api.Post(new Payload()
                   {
                       Id = 1,
                       Name = "Test"
                   }),
                   request =>
                   {
                       var content = request.Content.ReadAsStringAsync().Result;
                       Assert.Equal("<Payload xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Id>1</Id><Name>Test</Name></Payload>", content);
                   });
        }

        [Fact]
        public void TestCustomizedXmlSerialization()
        {
            TestTheRequest<IApi>(
                   api => api.PostCustomized(new CustomizedPayload
                   {
                       Id = 1,
                       Name = "Test"
                   }),
                   request =>
                   {
                       var content = request.Content.ReadAsStringAsync().Result;
                       Assert.Equal("<Payload xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" Name=\"Test\"><Id>1</Id><refresh_token>my-token</refresh_token></Payload>", content);
                   });
        }

        public class Payload
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [XmlRoot(ElementName = "Payload")]
        public class CustomizedPayload
        {

            public int Id { get; set; }
            [System.Xml.Serialization.XmlAttribute]
            public string Name { get; set; }

            [XmlElement(ElementName = "refresh_token")]
            public string RefreshToken { get; set; } = "my-token";
        }
    }
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
                       var content = request.Content.ReadAsStringAsync().Result;
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
                       var content = request.Content.ReadAsStringAsync().Result;
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
