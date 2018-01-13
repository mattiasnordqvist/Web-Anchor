using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebAnchor
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<TReturn> Content<T, TReturn>(this HttpResponseMessage @this, Func<T, TReturn> projection)
        {
            var result = await @this.Content.ReadAsStringAsync().ConfigureAwait(false);
            var content = JsonConvert.DeserializeObject<T>(result);
            return projection(content);
        }
    }
}