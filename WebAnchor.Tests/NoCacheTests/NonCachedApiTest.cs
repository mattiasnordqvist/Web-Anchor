using System.Net.Http;

using NUnit.Framework;

namespace WebAnchor.Tests.NoCacheTests
{
    [TestFixture]
    public class NonCachedApiTest : WebAnchorTest
    {
        [Test]
        public void TestOnApiLevel()
        {
            this.Test<INonCachedApi>(
                api => api.Get(),
                m =>
                    {
                        Assert.AreEqual(HttpMethod.Get, m.Method);
                        Assert.IsTrue(m.RequestUri.ToString().Contains("_="));
                    });
        }

        [Test]
        public void TestOnMethodLevel_IsCached()
        {
            this.Test<ISomeCachedApi>(
                api => api.Cached(),
                m =>
                {
                    Assert.AreEqual(HttpMethod.Get, m.Method);
                    Assert.IsTrue(!m.RequestUri.ToString().Contains("_="));
                });
        }

        [Test]
        public void TestOnMethodLevel_IsNotCached()
        {
            this.Test<ISomeCachedApi>(
                api => api.NotCached(),
                m =>
                {
                    Assert.AreEqual(HttpMethod.Get, m.Method);
                    Assert.IsTrue(m.RequestUri.ToString().Contains("_="));
                });
        }
    }
}