using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using NUnit.Framework;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.HttpAttributes;
using WebAnchor.RequestFactory.Transformation.Transformers.Headers.Dynamic;
using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.RequestFactory.Headers
{
    [TestFixture]
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

        [Test]
        public void TestGenericHeaderUsingParameterName()
        {
            TestTheRequest<IApi>(
                   api => api.Get("Basic 79iou342qkras9"),
                   request =>
                   {
                       Assert.AreEqual("location/resource", request.RequestUri.ToString());
                       Assert.That(request.Headers.Contains("Authorization"));
                       Assert.AreEqual("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                   });
        }

        [Test]
        public void TestGenericHeaderWithGivenName()
        {
            TestTheRequest<IApi>(
                   api => api.Get2("Basic 79iou342qkras9"),
                   request =>
                   {
                       Assert.AreEqual("location/resource", request.RequestUri.ToString());
                       Assert.That(request.Headers.Contains("Authorization"));
                       Assert.AreEqual("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                   });
        }

        [Test]
        public void TestSpecializedHeader()
        {
            TestTheRequest<IApi>(
                   api => api.Get2("Basic 79iou342qkras9"),
                   request =>
                   {
                       Assert.AreEqual("location/resource", request.RequestUri.ToString());
                       Assert.That(request.Headers.Contains("Authorization"));
                       Assert.AreEqual("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                   });
        }
    }
}
