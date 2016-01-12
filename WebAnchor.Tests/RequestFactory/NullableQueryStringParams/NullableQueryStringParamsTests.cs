using System.Net.Http;

using NUnit.Framework;

using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.RequestFactory.NullableQueryStringParams
{
    [TestFixture]
    public class NullableQueryStringParamsTests : WebAnchorTest
    {
        [Test]
        public void NullableIntWithNullValueShouldBeOmitted()
        {
            TestTheRequest<IApi>(
                api => api.Get(null),
                m =>
                    {
                        Assert.AreEqual(HttpMethod.Get, m.Method);
                        Assert.AreEqual("base/path1", m.RequestUri.ToString());
                    });
        }

        [Test]
        public void NullableIntWithValueShouldNotBeOmitted()
        {
            TestTheRequest<IApi>(
                api => api.Get(1),
                m =>
                {
                    Assert.AreEqual(HttpMethod.Get, m.Method);
                    Assert.AreEqual("base/path1?i=1", m.RequestUri.ToString());
                });
        }

        [Test]
        public void NonNullableStringWithNullValueShouldBeOmitted()
        {
            TestTheRequest<IApi>(
                api => api.Get2(null),
                m =>
                {
                    Assert.AreEqual(HttpMethod.Get, m.Method);
                    Assert.AreEqual("base/path1", m.RequestUri.ToString());
                });
        }

        [Test]
        public void NonNullableStringWithValueShouldNotBeOmitted()
        {
            TestTheRequest<IApi>(
                api => api.Get2("1"),
                m =>
                {
                    Assert.AreEqual(HttpMethod.Get, m.Method);
                    Assert.AreEqual("base/path1?s=1", m.RequestUri.ToString());
                });
        }
    }
}
