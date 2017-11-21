using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.HttpAttributes;
using WebAnchor.RequestFactory.Transformation.Transformers.Headers.Dynamic;
using WebAnchor.Tests.TestUtils;

using Xunit;

namespace WebAnchor.Tests.RequestFactory.Headers
{
    public class HeaderAttributeOnParameterTest : WebAnchorTest
    {
        [BaseLocation("location")]
        public interface IApi
        {
            [Get("resource")]
            Task<HttpResponseMessage> Get([Header]string authorization);

            [Get("resource")]
            Task<HttpResponseMessage> Get2([Header("Authorization")]string value);

            [Get("resource")]
            Task<HttpResponseMessage> Get3([Authorization]string value);
        }

        [Fact]
        public void TestGenericHeaderUsingParameterName()
        {
            TestTheRequest<IApi>(
                   api => api.Get("Basic 79iou342qkras9"),
                   request =>
                   {
                       Assert.Equal("location/resource", request.RequestUri.ToString());
                       Assert.True(request.Headers.Contains("Authorization"));
                       Assert.Equal("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                   });
        }

        [Fact]
        public void TestGenericHeaderWithGivenName()
        {
            TestTheRequest<IApi>(
                   api => api.Get2("Basic 79iou342qkras9"),
                   request =>
                   {
                       Assert.Equal("location/resource", request.RequestUri.ToString());
                       Assert.True(request.Headers.Contains("Authorization"));
                       Assert.Equal("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                   });
        }

        [Fact]
        public void TestSpecializedHeader()
        {
            TestTheRequest<IApi>(
                   api => api.Get2("Basic 79iou342qkras9"),
                   request =>
                   {
                       Assert.Equal("location/resource", request.RequestUri.ToString());
                       Assert.True(request.Headers.Contains("Authorization"));
                       Assert.Equal("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                   });
        }
    }
}
