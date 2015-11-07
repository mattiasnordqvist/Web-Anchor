using System.Net.Http;
using System.Threading.Tasks;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.HttpAttributes;

namespace WebAnchor.Tests.RequestFactory.Url
{
    [BaseLocation("base")]
    public interface IApi
    {
        [Get("/path1")]
        Task<HttpResponseMessage> Get();
        
        [Get("path2")]
        Task<HttpResponseMessage> Get2();
    }
}