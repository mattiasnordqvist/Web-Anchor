using System;

using NUnit.Framework;

namespace WebAnchor.Tests.Validation
{
    [TestFixture]
    public class MisuseTests
    {
        [Test]
        public void ApiForConcreteClassThrowsWebAnchorException()
        {
            Assert.Throws<WebAnchorException>(() => Api.For<ConcreteClass>("http://localhost:1111"));
        }

        [Test]
        public void ApiForMumboJumboUrlThrowsUriFormatException()
        {
            Assert.Throws<UriFormatException>(() => Api.For<IInterface>("asdfnoin"));
        }
    }
}
