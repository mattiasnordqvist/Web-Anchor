using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAnchor
{
    public interface IHttpClient : IDisposable
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}