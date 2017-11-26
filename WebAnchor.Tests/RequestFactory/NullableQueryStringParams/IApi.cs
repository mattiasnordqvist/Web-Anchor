using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;

namespace WebAnchor.Tests.RequestFactory.NullableQueryStringParams
{
    [BaseLocation("base")]
    public interface IApi
    {
        [Get("/path1")]
        Task<HttpResponseMessage> Get(int? i);

        [Get("/path1")]
        Task<HttpResponseMessage> Get2(string s);
    }
}