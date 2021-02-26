using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.Header;
using WebAnchor.Attributes.URL;
using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.Wiki
{
    public class HeadersTests : WebAnchorTest
    {
        [BaseLocation("api")]
        public interface IApi
        {
            [Get("resource")]
            Task<HttpResponseMessage> Get([Header] string authorization);
        }

        [Fact]
        public void TestGetPosts()
        {
            TestTheRequest<IApi>(
               api => api.Get("Basic 987asdg676g"),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api/resource", assertMe.RequestUri?.ToString());
                   Assert.Equal("Basic 987asdg676g", assertMe.Headers.GetValues("Authorization").First());
               });
        }
    }
}