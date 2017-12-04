using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;
using WebAnchor.RequestFactory.Transformation.Transformers.Headers;
using WebAnchor.Tests.TestUtils;

using Xunit;

namespace WebAnchor.Tests.RequestFactory.Headers
{
    public class HeaderAttributeOnMethodTest : WebAnchorTest
    {
        [BaseLocation("location")]
        public interface IApi
        {
            [Get("resource")]
            [Header("Authorization", "Basic 79iou342qkras9")]
            Task<HttpResponseMessage> Get();

            [Get("resource")]
            [Authorization("Basic 79iou342qkras9")]
            Task<HttpResponseMessage> Get2();
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
            TestTheRequest<IApi>(
                   api => api.Get2(),
                   request =>
                   {
                       Assert.True(request.Headers.Contains("Authorization"));
                       Assert.Equal("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                   });
        }
    }
}
