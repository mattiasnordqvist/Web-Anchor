using System;

using Xunit;

namespace WebAnchor.Tests.Validation
{
    public class MisuseTests
    {
        [Fact]
        public void ApiForConcreteClassThrowsWebAnchorException()
        {
            Assert.Throws<WebAnchorException>(() => Api.For<ConcreteClass>("http://localhost:1111"));
        }

        [Fact]
        public void ApiForMumboJumboUrlThrowsUriFormatException()
        {
            Assert.Throws<UriFormatException>(() => Api.For<IInterface>("asdfnoin"));
        }
    }
}
