using System;

using NUnit.Framework;

namespace WebAnchor.Tests
{
    [TestFixture]
    public class MisuseTests
    {
        [Test]
        [ExpectedException(typeof(WebAnchorException))]
        public void ApiForConcreteClassThrowsWebAnchorException()
        {
            Api.For<ConcreteClass>("http://localhost:1111");
        }

        [Test]
        [ExpectedException(typeof(UriFormatException))]
        public void ApiForMumboJumboUrlThrowsUriFormatException()
        {
            Api.For<IInterface>("asdfnoin");
        }
    }
}
