using WebAnchor.RequestFactory.HttpAttributes;
using WebAnchor.RequestFactory.Transformers;

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