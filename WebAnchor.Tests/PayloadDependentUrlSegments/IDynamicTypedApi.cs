using System.Net.Http;
using System.Threading.Tasks;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.HttpAttributes;

namespace WebAnchor.Tests.PayloadDependentUrlSegments
{
    [BaseLocation("/api")]
    [TypeNameAsUrlParameter]
    public interface IDynamicTypedApi<in T>
    {
        [Post("/{payloadType}")]
        Task<HttpResponseMessage> PostThis([Payload]T t);
    }
}