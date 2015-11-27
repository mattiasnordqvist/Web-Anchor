using FakeItEasy;

using NUnit.Framework;

using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.Disposable
{
    [TestFixture]
    public class DisposableTests : WebAnchorTest
    {
        [Test]
        public void ShouldDispose()
        {
            var fakeHttpClient = A.Fake<IHttpClient>();
            var api = new ApiFactory().Create<IApi>(fakeHttpClient, true);
            api.Dispose();
            A.CallTo(() => fakeHttpClient.Dispose()).MustHaveHappened();
        }

        [Test]
        public void ShouldNotDispose()
        {
            var fakeHttpClient = A.Fake<IHttpClient>();
            var api = new ApiFactory().Create<IApi>(fakeHttpClient, false);
            api.Dispose();
            A.CallTo(() => fakeHttpClient.Dispose()).MustNotHaveHappened();
        }

        [Test]
        public void ShouldAlsoNotDispose()
        {
            var fakeHttpClient = A.Fake<IHttpClient>();
            new ApiFactory().Create<IApi>(fakeHttpClient, true);
            A.CallTo(() => fakeHttpClient.Dispose()).MustNotHaveHappened();
        }
    }
}