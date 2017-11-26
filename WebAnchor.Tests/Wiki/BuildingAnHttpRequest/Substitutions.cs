using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;
using WebAnchor.Tests.TestUtils;
using Xunit;

namespace WebAnchor.Tests.Wiki
{
    public class Substitutions : WebAnchorTest
    {
        [BaseLocation("api/{resource}")]
        public interface IMyApi
        {
            [Get("{id}")]
            Task<HttpResponseMessage> Get(string resource, int id);
        }

        [Fact]
        public void TestGetPosts()
        {
            TestTheRequest<IMyApi>(
               api => api.Get("posts", 82),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api/posts/82", assertMe.RequestUri.ToString());
               });
        }
    }
}