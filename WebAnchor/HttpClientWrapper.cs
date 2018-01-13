using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebAnchor
{
    public class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Uri BaseAddress { get { return _httpClient.BaseAddress; } set { _httpClient.BaseAddress = value; } }
        public long MaxResponseContentBufferSize { get { return _httpClient.MaxResponseContentBufferSize; } set { _httpClient.MaxResponseContentBufferSize = value; } }
        public TimeSpan Timeout { get { return _httpClient.Timeout; } set { _httpClient.Timeout = value; } }
        public void CancelPendingRequests() { _httpClient.CancelPendingRequests(); }
        public async Task<HttpResponseMessage> DeleteAsync(string requestUri) => await _httpClient.DeleteAsync(requestUri).ConfigureAwait(false);
        public async Task<HttpResponseMessage> DeleteAsync(Uri requestUri) => await _httpClient.DeleteAsync(requestUri).ConfigureAwait(false);
        public async Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken) => await _httpClient.DeleteAsync(requestUri, cancellationToken).ConfigureAwait(false);
        public async Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken) => await _httpClient.DeleteAsync(requestUri, cancellationToken).ConfigureAwait(false);
        public void Dispose() => _httpClient.Dispose();
        public override bool Equals(object obj) => _httpClient.Equals(obj);
        public async Task<HttpResponseMessage> GetAsync(string requestUri) => await _httpClient.GetAsync(requestUri).ConfigureAwait(false);
        public async Task<HttpResponseMessage> GetAsync(Uri requestUri) => await _httpClient.GetAsync(requestUri).ConfigureAwait(false);
        public async Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption) => await _httpClient.GetAsync(requestUri, completionOption).ConfigureAwait(false);
        public async Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken) => await _httpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
        public async Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption) => await _httpClient.GetAsync(requestUri, completionOption).ConfigureAwait(false);
        public async Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken) => await _httpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
        public async Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken) => await _httpClient.GetAsync(requestUri, completionOption, cancellationToken).ConfigureAwait(false);
        public async Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken) => await _httpClient.GetAsync(requestUri, completionOption, cancellationToken).ConfigureAwait(false);
        public async Task<byte[]> GetByteArrayAsync(string requestUri) => await _httpClient.GetByteArrayAsync(requestUri).ConfigureAwait(false);
        public async Task<byte[]> GetByteArrayAsync(Uri requestUri) => await _httpClient.GetByteArrayAsync(requestUri).ConfigureAwait(false);
        public override int GetHashCode() => _httpClient.GetHashCode();
        public async Task<Stream> GetStreamAsync(string requestUri) => await _httpClient.GetStreamAsync(requestUri).ConfigureAwait(false);
        public async Task<Stream> GetStreamAsync(Uri requestUri) => await _httpClient.GetStreamAsync(requestUri).ConfigureAwait(false);
        public async Task<string> GetStringAsync(string requestUri) => await _httpClient.GetStringAsync(requestUri).ConfigureAwait(false);
        public async Task<string> GetStringAsync(Uri requestUri) => await _httpClient.GetStringAsync(requestUri).ConfigureAwait(false);
        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content) => await _httpClient.PostAsync(requestUri, content).ConfigureAwait(false);
        public async Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content) => await _httpClient.PostAsync(requestUri, content).ConfigureAwait(false);
        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken) => await _httpClient.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        public async Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken) => await _httpClient.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        public async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content) => await _httpClient.PutAsync(requestUri, content).ConfigureAwait(false);
        public async Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content) => await _httpClient.PutAsync(requestUri, content).ConfigureAwait(false);
        public async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken) => await _httpClient.PutAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        public async Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken) => await _httpClient.PutAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) => await _httpClient.SendAsync(request).ConfigureAwait(false);
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption) => await _httpClient.SendAsync(request, completionOption).ConfigureAwait(false);
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken) => await _httpClient.SendAsync(request, completionOption, cancellationToken).ConfigureAwait(false);
        public override string ToString() => _httpClient.ToString();
    }
}