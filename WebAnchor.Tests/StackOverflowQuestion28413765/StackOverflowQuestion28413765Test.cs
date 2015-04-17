using System.Collections.Generic;
using System.Net.Http;

using NUnit.Framework;

namespace WebAnchor.Tests.StackOverflowQuestion28413765
{
    [TestFixture]
    public class StackOverflowQuestion28413765Test : WebAnchorTest
    {
        [Test]
        public void StackoverflowQuestion28413765Test()
        {
            Test<IStackOverflowQuestion28413765>(api => api.GetAll(new List<TrackSubType> { TrackSubType.Type1, TrackSubType.Type3 }),
                m =>
                    {
                        Assert.AreEqual(HttpMethod.Get, m.Method);
                        Assert.AreEqual("/track/?content-type[]=Type1&content-type[]=Type3", m.RequestUri.ToString());
                    });
        }
    }
}