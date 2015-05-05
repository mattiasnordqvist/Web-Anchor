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
            Api.For<IApiWithVoid>("http://localhost/");
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
        [ExpectedException(typeof(WebAnchorException))]
        public void MethodsWithTaskOnlyAreNotAllowed()
        {
            Api.For<IApiWithTaskOnly>("http://localhost/");
        }
    }
}
