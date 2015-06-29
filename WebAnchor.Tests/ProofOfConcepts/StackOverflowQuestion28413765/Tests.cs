using System.Collections.Generic;
using System.Net.Http;

using NUnit.Framework;

using WebAnchor.Tests.ProofOfConcepts.StackOverflowQuestion28413765.Fixtures;
using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.ProofOfConcepts.StackOverflowQuestion28413765
{
    [TestFixture]
    public class Tests : WebAnchorTest
    {
        [Test]
        public void StackoverflowQuestion28413765Test()
        {
            TestTheRequestMessage<IApi>(api => api.GetAll(new List<TrackSubType> { TrackSubType.Type1, TrackSubType.Type3 }),
                m =>
                    {
                        Assert.AreEqual(HttpMethod.Get, m.Method);
                        Assert.AreEqual("/track/?content-type[]=Type1&content-type[]=Type3", m.RequestUri.ToString());
                    });
        }
    }
}