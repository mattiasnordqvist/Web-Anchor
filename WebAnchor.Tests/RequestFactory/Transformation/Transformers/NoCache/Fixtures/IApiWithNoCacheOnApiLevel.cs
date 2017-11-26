using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;
using WebAnchor.RequestFactory.Transformation.Transformers.NoCache;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.NoCache.Fixtures
{
    [NoCache]
    public interface IApiWithNoCacheOnApiLevel
    {
        [Get("test")]
        Task<HttpRequestMessage> Get();
    }
}
