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
            var api = new ApiFactory().Create<IApi>(fakeHttpClient, true, new ApiSettings());
            api.Dispose();
            A.CallTo(() => fakeHttpClient.Dispose()).MustHaveHappened();
        }

        [Test]
        public void ShouldNotDisposeHttpClient_WhenHttpClientIsProvidedByConsumer()
        {
            var fakeHttpClient = A.Fake<IHttpClient>();
            var api = new ApiFactory().Create<IApi>(fakeHttpClient, false, new ApiSettings());
            api.Dispose();
            A.CallTo(() => fakeHttpClient.Dispose()).MustNotHaveHappened();
        }

        [Test]
        public void ShouldNeverDisposeHttpClient_WhenDisposeIsNotInvokedOnIApi()
        {
            var fakeHttpClient = A.Fake<IHttpClient>();
            new ApiFactory().Create<IApi>(fakeHttpClient, true, new ApiSettings());
            A.CallTo(() => fakeHttpClient.Dispose()).MustNotHaveHappened();
        }
    }
}