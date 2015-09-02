using System.Net.Http;

using NUnit.Framework;

using WebAnchor.Tests.RequestFactory.Transformation.Custom;
using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.RequestFactory.Url
{
    [TestFixture]
    public class UrlCreationTests : WebAnchorTest
    {
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
