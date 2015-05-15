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
            Test<INonCachedApi>(
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
            Test<ISomeCachedApi>(
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
            Test<ISomeCachedApi>(
                api => api.NotCached(),
                m =>
                {
                    Assert.AreEqual(HttpMethod.Get, m.Method);
                    Assert.IsTrue(m.RequestUri.ToString().Contains("_="));
                });
        }
    }
}