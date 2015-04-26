using WebAnchor.RequestFactory.HttpAttributes;
using WebAnchor.RequestFactory.Transformers;

namespace WebAnchor.Tests.NoCacheTests
{
    [NoCache]
    public interface INonCachedApi
    {
        [Get("test")]
        Customer Get();
    }
}
