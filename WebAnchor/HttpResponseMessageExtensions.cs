using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAnchor
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<TReturn> Content<T, TReturn>(this HttpResponseMessage @this, Func<T, TReturn> projection)
        {
            var result = await @this.Content.ReadAsStringAsync().ConfigureAwait(false);
            var content = JsonSerializer.Deserialize<T>(result);
            return projection(content);
        }
    }
}