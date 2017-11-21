using System.Collections.Generic;
using System.Net.Http;

using WebAnchor.Tests.ProofOfConcepts.StackOverflowQuestion28413765.Fixtures;
using WebAnchor.Tests.TestUtils;

using Xunit;

namespace WebAnchor.Tests.ProofOfConcepts.StackOverflowQuestion28413765
{
    public class Tests : WebAnchorTest
    {
        [Fact]
        public void StackoverflowQuestion28413765Test()
        {
            TestTheRequest<IApi>(api => api.GetAll(new List<TrackSubType> { TrackSubType.Type1, TrackSubType.Type3 }),
                m =>
                    {
                        Assert.Equal(HttpMethod.Get, m.Method);
                        Assert.Equal("/track?content-type[]=Type1&content-type[]=Type3", m.RequestUri.ToString());
                    });
        }
    }
}