using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.Content;
using WebAnchor.Attributes.URL;
using WebAnchor.Tests.TestUtils;
using Xunit;

namespace WebAnchor.Tests.Wiki
{
    public class BodyContentTests : WebAnchorTest
    {
        [BaseLocation("api")]
        public interface IApi
        {
            [Post("")]
            Task<HttpResponseMessage> PostPost([Content]Post post);
        }

        [Fact]
        public void TestPostPost()
        {
            TestTheRequest<IApi>(
               api => api.PostPost(new Post() { Id = 1, UserId = 2 }),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Post, assertMe.Method);
                   Assert.Equal("api", assertMe.RequestUri.ToString());
                   var body = assertMe.Content.ReadAsStringAsync().Result;
                   Assert.Equal(@"{""Id"":1,""UserId"":2}", body);
               });
        }

        public class Post
        {
            public int Id { get; set; }
            public int UserId { get; set; }
        }
    }
}