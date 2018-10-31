using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.RequestFactory.Url
{
    public class CustomHttpAttributeTests : WebAnchorTest
    {
        [Fact]
        public void CustomVerb()
        {
            TestTheRequest<IApi>(api => api.TestVerb(), m => 
            {
                Assert.Equal("TEST", m.Method.Method);
                Assert.Null(m.Content);
            });
        }

        [Fact]
        public void CustomVerb_WithContent()
        {
            TestTheRequest<IApi>(api => api.TestVerbWithContent(new TestContent { Message = "content" }), m =>
            {
                Assert.Equal("TEST", m.Method.Method);
                Assert.NotNull(m.Content);
            });
        }
    }
}
