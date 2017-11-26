using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;
using WebAnchor.RequestFactory.Transformation.Transformers.NoCache;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.NoCache.Fixtures
{
    public interface IApiWithBothCachedAndNonCachedMethods
    {
        [Get("cached")]
        Task<HttpRequestMessage> Cached();
        
        [Get("notcached"), NoCache]
        Task<HttpRequestMessage> NotCached();
    }
}