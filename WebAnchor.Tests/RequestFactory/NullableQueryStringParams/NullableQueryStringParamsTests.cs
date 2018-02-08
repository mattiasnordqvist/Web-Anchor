using System.Net.Http;

using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.RequestFactory.NullableQueryStringParams
{
    public class NullableQueryStringParamsTests : WebAnchorTest
    {
        [Fact]
        public void NullableIntWithNullValueShouldBeOmitted()
        {
            TestTheRequest<IApi>(
                api => api.Get(null),
                m =>
                    {
                        Assert.Equal(HttpMethod.Get, m.Method);
                        Assert.Equal("base/path1", m.RequestUri.ToString());
                    });
        }

        [Fact]
        public void NullableIntWithValueShouldNotBeOmitted()
        {
            TestTheRequest<IApi>(
                api => api.Get(1),
                m =>
                {
                    Assert.Equal(HttpMethod.Get, m.Method);
                    Assert.Equal("base/path1?i=1", m.RequestUri.ToString());
                });
        }

        [Fact]
        public void NonNullableStringWithNullValueShouldBeOmitted()
        {
            TestTheRequest<IApi>(
                api => api.Get2(null),
                m =>
                {
                    Assert.Equal(HttpMethod.Get, m.Method);
                    Assert.Equal("base/path1", m.RequestUri.ToString());
                });
        }

        [Fact]
        public void NonNullableStringWithValueShouldNotBeOmitted()
        {
            TestTheRequest<IApi>(
                api => api.Get2("1"),
                m =>
                {
                    Assert.Equal(HttpMethod.Get, m.Method);
                    Assert.Equal("base/path1?s=1", m.RequestUri.ToString());
                });
        }
    }
}
