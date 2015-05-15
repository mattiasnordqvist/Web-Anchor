using WebAnchor.RequestFactory.HttpAttributes;
using WebAnchor.RequestFactory.Transformation.Transformers.NoCache;

namespace WebAnchor.Tests.NoCacheTests
{
    public interface ISomeCachedApi
    {
        [Get("cached")]
        Customer Cached();
        
        [Get("notcached"), NoCache]
        Customer NotCached();
    }
}