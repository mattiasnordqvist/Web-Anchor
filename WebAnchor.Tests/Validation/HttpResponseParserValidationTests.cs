using NUnit.Framework;

using WebAnchor.RequestFactory.HttpAttributes;

namespace WebAnchor.Tests.Validation
{
    [TestFixture]
    public class HttpResponseParserValidationTests
    {
        [Test]
        [ExpectedException(typeof(WebAnchorException))]
        public void MethodsWithVoidAreNotAllowed()
        {
            Api.For<ApiWithVoid>("http://localhost/");
        }

        [Test]
        public void MethodsWithTaskOfHttpResponseMessageAreAllowed()
        {
            Api.For<ApiWithTaskOfHttpResponseMessage>("http://localhost/");
        }

        [Test]
        public void MethodsWithTaskOfTAreAllowed()
        {
            Api.For<ApiWithTaskOfT>("http://localhost/");
        }

        [Test]
        [ExpectedException(typeof(WebAnchorException))]
        public void MethodsWithTaskOnlyAreNotAllowed()
        {
            Api.For<ApiWithTaskOnly>("http://localhost/");
        }
    }
}
