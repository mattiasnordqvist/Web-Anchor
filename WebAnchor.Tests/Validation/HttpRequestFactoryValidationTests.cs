using Xunit;

namespace WebAnchor.Tests.Validation
{
    public class HttpRequestFactoryValidationTests
    {
        [Fact]
        public void MethodWithoutHttpMethodAttributeThrowsExceptions()
        {
            Assert.Throws<WebAnchorException>(() => Api.For<IApiWithoutHttpMethodAttribute>("http://localhost/"));
        }

        [Fact]
        public void MethodWithDuplicatedContentAttributesThrowsExceptions()
        {
            Assert.Throws<WebAnchorException>(() => Api.For<IApiWithMethodWithDuplicatedContentAttributes>("http://localhost/"));
        }
    }
}