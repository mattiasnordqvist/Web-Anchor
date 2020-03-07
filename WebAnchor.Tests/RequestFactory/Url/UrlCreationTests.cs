using System.Net.Http;

using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.RequestFactory.Url
{
    public class UrlCreationTests : WebAnchorTest
    {
        [Fact]
        public void PathSlashesAreNotUrlEncoded()
        {
            TestTheRequest<IApi>(
                api => api.Get3("a/path/with/preserved/slashes"),
                a =>
                    {
                        Assert.Equal(HttpMethod.Get, a.Method);
                        Assert.Equal("base/a/path/with/preserved/slashes", a.RequestUri.ToString());
                    });
        }

        [Fact]
        public void PathSlashesAreNotUrlEncoded_UnlessYouSaySo()
        {
            TestTheRequest<IApi>(
                api => api.Get3("a/path/with/url/encoded/slashes"),
                settings: new UrlEncodeSlashesSettings(), 
                assertHttpRequestMessage: a =>
                {
                    Assert.Equal(HttpMethod.Get, a.Method);
                    Assert.Equal("base/a%2Fpath%2Fwith%2Furl%2Fencoded%2Fslashes", a.RequestUri.ToString());
                });
        }

        [Fact]
        public void ConcatBaseLocationWithVerb()
        {
            TestTheRequest<IApi>(
                api => api.Get(),
                m =>
                    {
                        Assert.Equal(HttpMethod.Get, m.Method);
                        Assert.Equal("base/path1", m.RequestUri.ToString());
                    });
        }

        [Fact]
        public void ConcatBaseLocationWithVerb_SlashIsAppendedIfMissing()
        {
            TestTheRequest<IApi>(
                api => api.Get2(),
                m =>
                {
                    Assert.Equal(HttpMethod.Get, m.Method);
                    Assert.Equal("base/path2", m.RequestUri.ToString());
                });
        }

        [Fact]
        public void ConcatBaseLocationWithVerb_SlashIsAppendedIfMissingUnlessDisabled()
        {
            TestTheRequest<IApi>(
                api => api.Get2(),
                m =>
                {
                    Assert.Equal(HttpMethod.Get, m.Method);
                    Assert.Equal("basepath2", m.RequestUri.ToString());
                },
                configure: x => x.Request.InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl = false);
        }

        [Fact]
        public void DuplicateSlashesInCaseOfEmptySegments()
        {
            TestTheRequest<IApi>(
                api => api.Get4(""),
                a =>
                {
                    Assert.Equal(HttpMethod.Get, a.Method);
                    Assert.Equal("base/a//c", a.RequestUri.ToString());
                });
        }
    }
}
