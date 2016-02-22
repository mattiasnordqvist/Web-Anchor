using Castle.DynamicProxy;
using FakeItEasy;

using NUnit.Framework;

using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.Disposable
{
    [TestFixture]
    public class DisposableTests : WebAnchorTest
    {
        [Test]
        public void ShouldDisposeHttpClient_WhenHttpClientIsCreatedInternally()
        {
            var fakeHttpClient = A.Fake<IHttpClient>();
            var api = new ApiFactory(new ProxyGenerator()).Create<IApi>(fakeHttpClient, true);
            api.Dispose();
            A.CallTo(() => fakeHttpClient.Dispose()).MustHaveHappened();
        }

        [Test]
        public void ShouldNotDisposeHttpClient_WhenHttpClientIsProvidedByConsumer()
        {
            var fakeHttpClient = A.Fake<IHttpClient>();
            var api = new ApiFactory(new ProxyGenerator()).Create<IApi>(fakeHttpClient, false);
            api.Dispose();
            A.CallTo(() => fakeHttpClient.Dispose()).MustNotHaveHappened();
        }

        [Test]
        public void ShouldNeverDisposeHttpClient_WhenDisposeIsNotInvokedOnIApi()
        {
            var fakeHttpClient = A.Fake<IHttpClient>();
            new ApiFactory(new ProxyGenerator()).Create<IApi>(fakeHttpClient, true);
            A.CallTo(() => fakeHttpClient.Dispose()).MustNotHaveHappened();
        }
    }
}