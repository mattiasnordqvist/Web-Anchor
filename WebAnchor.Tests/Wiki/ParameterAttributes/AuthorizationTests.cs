using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.Header;
using WebAnchor.Attributes.URL;
using WebAnchor.Tests.TestUtils;
using Xunit;

namespace WebAnchor.Tests.Wiki
{
    public class AuthorizationTests : WebAnchorTest
    {
        [BaseLocation("api")]
        public interface IApi
        {
            [Get("resource")]
            Task<HttpResponseMessage> Get([Authorization] string basicAndtoken);
        }

        [Fact]
        public void TestAuthorizationHeader()
        {
            TestTheRequest<IApi>(
               api => api.Get("Basic 123891"),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api/resource", assertMe.RequestUri.ToString());
                   Assert.Equal("Basic 123891", assertMe.Headers.GetValues("Authorization").First());
               });
        }
    }
}
