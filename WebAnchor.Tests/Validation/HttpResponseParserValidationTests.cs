using Xunit;

namespace WebAnchor.Tests.Validation
{
    public class HttpResponseParserValidationTests
    {
        [Fact]
        public void MethodsWithVoidAreNotAllowed()
        {
            Assert.Throws<WebAnchorException>(() => Api.For<IApiWithVoid>("http://localhost/"));
        }

        [Fact]
        public void MethodsWithTaskOfHttpResponseMessageAreAllowed()
        {
            Api.For<IApiWithTaskOfHttpResponseMessage>("http://localhost/");
        }

        [Fact]
        public void MethodsWithTaskOfTAreAllowed()
        {
            Api.For<IApiWithTaskOfT>("http://localhost/");
        }

        [Fact]
        public void MethodsWithTaskOnlyAreNotAllowed()
        {
            Assert.Throws<WebAnchorException>(() => Api.For<IApiWithTaskOnly>("http://localhost/"));
        }
    }
}
