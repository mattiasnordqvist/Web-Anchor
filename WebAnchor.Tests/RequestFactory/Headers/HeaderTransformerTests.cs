using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;
using WebAnchor.RequestFactory.Transformation.Transformers.Headers;
using WebAnchor.Tests.TestUtils;

using Xunit;

namespace WebAnchor.Tests.RequestFactory.Headers
{
    public class HeaderTransformerTests : WebAnchorTest
    {
        [BaseLocation("location")]
        public interface IApi
        {
            [Get("resource")]
            Task<HttpResponseMessage> Get();
        }

        [BaseLocation("location")]
        [AddHeader("Authorization", "Basic 79iou342qkras9")]
        public interface IApi2
        {
            [Get("resource")]
            Task<HttpResponseMessage> Get();
        }

        [BaseLocation("location")]
        public interface IApi3
        {
            [Get("resource")]
            [AddHeader("Authorization", "Basic 79iou342qkras9")]
            Task<HttpResponseMessage> Get();
        }

        [Fact]
        public void TestAddHeaderTransformer()
        {
            TestTheRequest<IApi>(
                api => api.Get(),
                settings: new ApiSettings1(), 
                assertHttpRequestMessage: request =>
                    {
                        Assert.True(request.Headers.Contains("Authorization"));
                        Assert.Equal("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                    });
        }

        [Fact]
        public void TestAddHeaderAttributeOnApiTransformer()
        {
            TestTheRequest<IApi2>(
                api => api.Get(),
                assertHttpRequestMessage: request =>
                {
                    Assert.True(request.Headers.Contains("Authorization"));
                    Assert.Equal("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                });
        }

        [Fact]
        public void TestAddHeaderAttributeOnMethodTransformer()
        {
            TestTheRequest<IApi3>(
                api => api.Get(),
                assertHttpRequestMessage: request =>
                {
                    Assert.True(request.Headers.Contains("Authorization"));
                    Assert.Equal("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                });
        }

        [Fact]
        public void TestAddAuthorizationTransformer()
        {
            TestTheRequest<IApi>(
                api => api.Get(),
                settings: new ApiSettings2(),
                assertHttpRequestMessage: request =>
                    {
                        Assert.True(request.Headers.Contains("Authorization"));
                        Assert.Equal("Basic 79iou342qkras9", request.Headers.GetValues("Authorization").Single());
                    });
        }

        public class ApiSettings1 : DefaultApiSettings
        {
            public ApiSettings1()
            {
                Request.ParameterListTransformers.Add(new AddHeaderAttribute("Authorization", "Basic 79iou342qkras9"));
            }
        }

        public class ApiSettings2 : DefaultApiSettings
        {
            public ApiSettings2()
            {
                Request.ParameterListTransformers.Add(new AddAuthorizationAttribute("Basic 79iou342qkras9"));
            }
        }
    }
}