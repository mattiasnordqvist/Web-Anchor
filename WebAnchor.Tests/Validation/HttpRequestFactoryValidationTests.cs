using NUnit.Framework;

namespace WebAnchor.Tests.Validation
{
    [TestFixture]
    public class HttpRequestFactoryValidationTests
    {
        [Test]
        [ExpectedException(typeof(WebAnchorException))]
        public void MethodWithoutHttpMethodAttributeThrowsExceptions()
        {
            Api.For<IApiWithoutHttpMethodAttribute>("http://localhost/");
        }
    }
}