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
            TestTheRequest<IApi>(
                   api => api.Get2(),
                   request =>
                   {
                       Assert.That(request.Headers.Contains("Authorization"));
                       Assert.AreEqual("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                   });
        }
    }
}
