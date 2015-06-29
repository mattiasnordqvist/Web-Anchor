using System.Net.Http;

using NUnit.Framework;

using WebAnchor.Tests.RequestFactory.Transformation.Transformers.NoCache.Fixtures;
using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.NoCache
{
    [TestFixture]
    public class NoCacheTests : WebAnchorTest
    {
        [Test]
        public void TestWithNoCacheAttributeOnApiLevel()
        {
            TestTheRequestMessage<IApiWithNoCacheOnApiLevel>(
                api => api.Get(),
                req =>
                    {
                        Assert.AreEqual(HttpMethod.Get, req.Method);
                        Assert.IsTrue(req.RequestUri.ToString().Contains("_="));
                    });
        }

        [Test]
        public void TestOnMethodThatDoesNotHaveNoCacheAttribute()
        {
            TestTheRequestMessage<IApiWithBothCachedAndNonCachedMethods>(
                api => api.Cached(),
                req =>
                {
                    Assert.AreEqual(HttpMethod.Get, req.Method);
                    Assert.IsTrue(!req.RequestUri.ToString().Contains("_="));
                });
        }

        [Test]
        public void TestOnMethodThatDoHaveNoCacheAttribute()
        {
            TestTheRequestMessage<IApiWithBothCachedAndNonCachedMethods>(
                api => api.NotCached(),
                req =>
                {
                    Assert.AreEqual(HttpMethod.Get, req.Method);
                    Assert.IsTrue(req.RequestUri.ToString().Contains("_="));
                });
        }
    }
}