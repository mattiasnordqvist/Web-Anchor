using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;
using WebAnchor.RequestFactory.Transformation.Transformers.Headers;
using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.RequestFactory.Headers
{
    public class HeaderAttributeOnInterfaceTest : WebAnchorTest
    {
        [BaseLocation("location")]
        [AddHeader("Authorization", "Basic 79iou342qkras9")]
        public interface IApi
        {
            [Get("resource")]
            Task<HttpResponseMessage> Get();
        }

        [BaseLocation("location")]
        [AddAuthorizationAttribute("Basic 79iou342qkras9")]
        public interface IApi2
        {
            [Get("resource")]
            Task<HttpResponseMessage> Get();
        }

        [Fact]
        public void TestGenericHeader()
        {
            TestTheRequest<IApi>(
                   api => api.Get(),
                   request =>
                   {
                       Assert.True(request.Headers.Contains("Authorization"));
                       Assert.Equal("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                   });
        }

        [Fact]
        public void TestSpecializedHeader()
        {
            TestTheRequest<IApi2>(
                   api => api.Get(),
                   request =>
                   {
                       Assert.True(request.Headers.Contains("Authorization"));
                       Assert.Equal("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                   });
        }
    }
}
