using FakeItEasy;

using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.Disposable
{
    public class DisposableTests : WebAnchorTest
    {
        [Fact]
        public void ShouldDisposeHttpClient_WhenHttpClientIsCreatedInternally()
        {
            var fakeHttpClient = A.Fake<IHttpClient>();
            var api = new ApiFactory().Create<IApi>(fakeHttpClient, true, new DefaultApiSettings());
            api.Dispose();
            A.CallTo(() => fakeHttpClient.Dispose()).MustHaveHappened();
        }

        [Fact]
        public void ShouldNotDisposeHttpClient_WhenHttpClientIsProvidedByConsumer()
        {
            var fakeHttpClient = A.Fake<IHttpClient>();
            var api = new ApiFactory().Create<IApi>(fakeHttpClient, false, new DefaultApiSettings());
            api.Dispose();
            A.CallTo(() => fakeHttpClient.Dispose()).MustNotHaveHappened();
        }

        [Fact]
        public void ShouldNeverDisposeHttpClient_WhenDisposeIsNotInvokedOnIApi()
        {
            var fakeHttpClient = A.Fake<IHttpClient>();
            new ApiFactory().Create<IApi>(fakeHttpClient, true, new DefaultApiSettings());
            A.CallTo(() => fakeHttpClient.Dispose()).MustNotHaveHappened();
        }
    }
}