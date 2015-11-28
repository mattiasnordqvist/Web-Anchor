using System.Net.Http;
using System.Threading.Tasks;

namespace WebAnchor
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
        void Dispose();
    }
}