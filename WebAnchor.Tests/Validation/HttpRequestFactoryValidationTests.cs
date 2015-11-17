using NUnit.Framework;

namespace WebAnchor.Tests.Validation
{
    [TestFixture]
    public class HttpRequestFactoryValidationTests
    {
        [Test]
        public void MethodWithoutHttpMethodAttributeThrowsExceptions()
        {
            Assert.Throws<WebAnchorException>(() => Api.For<IApiWithoutHttpMethodAttribute>("http://localhost/"));
        }

        [Test]
        public void MethodWithDuplicatedContentAttributesThrowsExceptions()
        {
            Assert.Throws<WebAnchorException>(() => Api.For<IApiWithMethodWithDuplicatedContentAttributes>("http://localhost/"));
        }
    }
}