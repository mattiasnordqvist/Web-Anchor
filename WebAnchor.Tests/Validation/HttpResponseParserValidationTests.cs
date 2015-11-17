using NUnit.Framework;

using WebAnchor.RequestFactory.HttpAttributes;

namespace WebAnchor.Tests.Validation
{
    [TestFixture]
    public class HttpResponseParserValidationTests
    {
        [Test]
        public void MethodsWithVoidAreNotAllowed()
        {
            Assert.Throws<WebAnchorException>(() => Api.For<IApiWithVoid>("http://localhost/"));
        }

        [Test]
        public void MethodsWithTaskOfHttpResponseMessageAreAllowed()
        {
            Api.For<IApiWithTaskOfHttpResponseMessage>("http://localhost/");
        }

        [Test]
        public void MethodsWithTaskOfTAreAllowed()
        {
            Api.For<IApiWithTaskOfT>("http://localhost/");
        }

        [Test]
        public void MethodsWithTaskOnlyAreNotAllowed()
        {
            Assert.Throws<WebAnchorException>(() => Api.For<IApiWithTaskOnly>("http://localhost/"));
        }
    }
}
