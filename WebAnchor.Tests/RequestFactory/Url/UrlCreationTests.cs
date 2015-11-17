using System.Net.Http;

using NUnit.Framework;

using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.RequestFactory.Url
{
    [TestFixture]
    public class UrlCreationTests : WebAnchorTest
    {
        [Test]
        public void PathSlashesAreNotUrlEncoded()
        {
            TestTheRequest<IApi>(
                api => api.Get3("a/path/with/preserved/slashes"),
                a =>
                    {
                        Assert.AreEqual(HttpMethod.Get, a.Method);
                        Assert.AreEqual("base/a/path/with/preserved/slashes", a.RequestUri.ToString());
                    });
        }

        [Test]
        public void PathSlashesAreNotUrlEncoded_UnlessYouSaySo()
        {
            TestTheRequest<IApi>(
                api => api.Get3("a/path/with/url/encoded/slashes"),
                settings: new UrlEncodeSlashesSettings(), 
                assertHttpRequestMessage: a =>
                {
                    Assert.AreEqual(HttpMethod.Get, a.Method);
                    Assert.AreEqual("base/a%2Fpath%2Fwith%2Furl%2Fencoded%2Fslashes", a.RequestUri.ToString());
                });
        }

        [Test]
        public void ConcatBaseLocationWithVerb()
        {
            TestTheRequest<IApi>(
                api => api.Get(),
                m =>
                    {
                        Assert.AreEqual(HttpMethod.Get, m.Method);
                        Assert.AreEqual("base/path1", m.RequestUri.ToString());
                    });
        }

        [Test]
        public void ConcatBaseLocationWithVerb_SlashIsAppendedIfMissing()
        {
            TestTheRequest<IApi>(
                api => api.Get2(),
                m =>
                {
                    Assert.AreEqual(HttpMethod.Get, m.Method);
                    Assert.AreEqual("base/path2", m.RequestUri.ToString());
                });
        }

        [Test]
        public void ConcatBaseLocationWithVerb_SlashIsAppendedIfMissingUnlessDisabled()
        {
            TestTheRequest<IApi>(
                api => api.Get2(),
                m =>
                {
                    Assert.AreEqual(HttpMethod.Get, m.Method);
                    Assert.AreEqual("basepath2", m.RequestUri.ToString());
                },
                settings: new ApiSettings
                {
                    InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl = false
                });
        }
    }
}
