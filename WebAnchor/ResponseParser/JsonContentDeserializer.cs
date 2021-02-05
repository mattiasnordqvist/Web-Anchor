using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAnchor.ResponseParser
{
    public class JsonContentDeserializer : IContentDeserializer
    {
        private readonly JsonSerializerOptions _options;

        public JsonContentDeserializer(JsonSerializerOptions options = null)
        {
            _options = options;
        }

        public virtual async Task<T> Deserialize<T>(Stream stream, HttpResponseMessage message)
        {
            return await JsonSerializer.DeserializeAsync<T>(stream, _options).ConfigureAwait(false);
        }
    }
}