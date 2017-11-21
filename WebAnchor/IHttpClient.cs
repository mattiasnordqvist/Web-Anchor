using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAnchor
{
    /// <summary>
    /// Not intended to be used by WebAnchor consumers.
    /// </summary>
    public interface IHttpClient : IDisposable
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}