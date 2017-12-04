using System.Collections.Generic;
using System.Threading.Tasks;
using WebAnchor.Attributes.Parameters;
using WebAnchor.Attributes.URL;

namespace WebAnchor.Tests.ProofOfConcepts.StackOverflowQuestion28413765.Fixtures
{
    public interface IApi
    {
        [Get("/track/")]
        Task<IList<Track>> GetAll([Alias("content-type[]")] IEnumerable<TrackSubType> contentTypes = null);
    }
}