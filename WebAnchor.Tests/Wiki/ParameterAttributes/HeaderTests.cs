using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.Header;
using WebAnchor.Attributes.URL;
using WebAnchor.Tests.TestUtils;
using Xunit;

namespace WebAnchor.Tests.Wiki
{
    public class HeaderTests : WebAnchorTest
    {
        [BaseLocation("api")]
        public interface IApi
        {
            [Get("resource")]
            Task<HttpResponseMessage> GetImplicit([Header] string authorization);

            [Get("resource")]
            Task<HttpResponseMessage> GetExplicit([Header("Authorization")] string auth);
        }

        [Fact]
        public void TestImplicitHeaderName()
        {
            TestTheRequest<IApi>(
               api => api.GetImplicit("Basic 123891"),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api/resource", assertMe.RequestUri.ToString());
                   Assert.Equal("Basic 123891", assertMe.Headers.GetValues("Authorization").First());
               });
        }

        [Fact]
        public void TestExplicitHeaderName()
        {
            TestTheRequest<IApi>(
               api => api.GetExplicit("Basic 123891"),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api/resource", assertMe.RequestUri.ToString());
                   Assert.Equal("Basic 123891", assertMe.Headers.GetValues("Authorization").First());
               });
        }
    }
}
