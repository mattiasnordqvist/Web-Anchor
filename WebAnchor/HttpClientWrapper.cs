using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebAnchor
{
    public class HttpClientWrapper : IHttpClient
    {
        protected readonly HttpClient HttpClient;

        public HttpClientWrapper(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public virtual Uri BaseAddress { get { return HttpClient.BaseAddress; } set { HttpClient.BaseAddress = value; } }
        public virtual long MaxResponseContentBufferSize { get { return HttpClient.MaxResponseContentBufferSize; } set { HttpClient.MaxResponseContentBufferSize = value; } }
        public virtual TimeSpan Timeout { get { return HttpClient.Timeout; } set { HttpClient.Timeout = value; } }
        public virtual void CancelPendingRequests() { HttpClient.CancelPendingRequests(); }
        public virtual async Task<HttpResponseMessage> DeleteAsync(string requestUri) => await HttpClient.DeleteAsync(requestUri).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> DeleteAsync(Uri requestUri) => await HttpClient.DeleteAsync(requestUri).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken) => await HttpClient.DeleteAsync(requestUri, cancellationToken).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken) => await HttpClient.DeleteAsync(requestUri, cancellationToken).ConfigureAwait(false);
        public virtual void Dispose() => HttpClient.Dispose();
        public override bool Equals(object obj) => HttpClient.Equals(obj);
        public virtual async Task<HttpResponseMessage> GetAsync(string requestUri) => await HttpClient.GetAsync(requestUri).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> GetAsync(Uri requestUri) => await HttpClient.GetAsync(requestUri).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption) => await HttpClient.GetAsync(requestUri, completionOption).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken) => await HttpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption) => await HttpClient.GetAsync(requestUri, completionOption).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken) => await HttpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken) => await HttpClient.GetAsync(requestUri, completionOption, cancellationToken).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken) => await HttpClient.GetAsync(requestUri, completionOption, cancellationToken).ConfigureAwait(false);
        public virtual async Task<byte[]> GetByteArrayAsync(string requestUri) => await HttpClient.GetByteArrayAsync(requestUri).ConfigureAwait(false);
        public virtual async Task<byte[]> GetByteArrayAsync(Uri requestUri) => await HttpClient.GetByteArrayAsync(requestUri).ConfigureAwait(false);
        public override int GetHashCode() => HttpClient.GetHashCode();
        public virtual async Task<Stream> GetStreamAsync(string requestUri) => await HttpClient.GetStreamAsync(requestUri).ConfigureAwait(false);
        public virtual async Task<Stream> GetStreamAsync(Uri requestUri) => await HttpClient.GetStreamAsync(requestUri).ConfigureAwait(false);
        public virtual async Task<string> GetStringAsync(string requestUri) => await HttpClient.GetStringAsync(requestUri).ConfigureAwait(false);
        public virtual async Task<string> GetStringAsync(Uri requestUri) => await HttpClient.GetStringAsync(requestUri).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content) => await HttpClient.PostAsync(requestUri, content).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content) => await HttpClient.PostAsync(requestUri, content).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken) => await HttpClient.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken) => await HttpClient.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content) => await HttpClient.PutAsync(requestUri, content).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content) => await HttpClient.PutAsync(requestUri, content).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken) => await HttpClient.PutAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken) => await HttpClient.PutAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) => await HttpClient.SendAsync(request).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption) => await HttpClient.SendAsync(request, completionOption).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        public virtual async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken) => await HttpClient.SendAsync(request, completionOption, cancellationToken).ConfigureAwait(false);
        public override string ToString() => HttpClient.ToString();
    }
}