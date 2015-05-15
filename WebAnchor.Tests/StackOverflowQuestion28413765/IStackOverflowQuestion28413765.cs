using System.Collections.Generic;
using System.Threading.Tasks;

using WebAnchor.RequestFactory.HttpAttributes;
using WebAnchor.RequestFactory.Resolvers;

namespace WebAnchor.Tests.StackOverflowQuestion28413765
{
    public interface IStackOverflowQuestion28413765
    {
        [Get("/track/")]
        Task<IList<Track>> GetAll([Alias("content-type[]")] IEnumerable<TrackSubType> contentTypes = null);
    }
}