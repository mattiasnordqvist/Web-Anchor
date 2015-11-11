using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using NUnit.Framework;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.HttpAttributes;
using WebAnchor.RequestFactory.Transformation.Transformers.Headers;
using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.RequestFactory.Headers
{
    [TestFixture]
    public class HeaderAttributeOnInterfaceTest : WebAnchorTest
    {
        [BaseLocation("location")]
        [Header("Authorization", "Basic 79iou342qkras9")]
        public interface IApi
        {
            [Get("resource")]
            Task<HttpResponseMessage> Get();
        }

        [BaseLocation("location")]
        [Authorization("Basic 79iou342qkras9")]
        public interface IApi2
        {
            [Get("resource")]
            Task<HttpResponseMessage> Get();
        }

        [Test]
        public void TestGenericHeader()
        {
            TestTheRequest<IApi>(
                   api => api.Get(),
                   request =>
                   {
                       Assert.That(request.Headers.Contains("Authorization"));
                       Assert.AreEqual("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                   });
        }

        [Test]
        public void TestSpecializedHeader()
        {
            TestTheRequest<IApi2>(
                   api => api.Get(),
                   request =>
                   {
                       Assert.That(request.Headers.Contains("Authorization"));
                       Assert.AreEqual("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                   });
        }
    }
}
