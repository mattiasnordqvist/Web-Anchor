using Castle.DynamicProxy;
using FakeItEasy;

using Xunit;

using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.Disposable
{
    public class DisposableTests : WebAnchorTest
    {
        [Fact]
        public void ShouldDisposeHttpClient_WhenHttpClientIsCreatedInternally()
        {
            var fakeHttpClient = A.Fake<IHttpClient>();
            var api = new ApiFactory(new ProxyGenerator()).Create<IApi>(fakeHttpClient, true);
            api.Dispose();
            A.CallTo(() => fakeHttpClient.Dispose()).MustHaveHappened();
        }

        [Fact]
        public void ShouldNotDisposeHttpClient_WhenHttpClientIsProvidedByConsumer()
        {
            var fakeHttpClient = A.Fake<IHttpClient>();
            var api = new ApiFactory(new ProxyGenerator()).Create<IApi>(fakeHttpClient, false);
            api.Dispose();
            A.CallTo(() => fakeHttpClient.Dispose()).MustNotHaveHappened();
        }

        [Fact]
        public void ShouldNeverDisposeHttpClient_WhenDisposeIsNotInvokedOnIApi()
        {
            var fakeHttpClient = A.Fake<IHttpClient>();
            new ApiFactory(new ProxyGenerator()).Create<IApi>(fakeHttpClient, true);
            A.CallTo(() => fakeHttpClient.Dispose()).MustNotHaveHappened();
        }
    }
}