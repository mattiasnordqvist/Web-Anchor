using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAnchor.ResponseParser
{
    public class JsonContentDeserializer : IContentDeserializer
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public JsonContentDeserializer(JsonSerializerOptions jsonSerializerOptions)
        {
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public virtual ValueTask<T> Deserialize<T>(Stream stream, HttpResponseMessage message)
        {
            return JsonSerializer.DeserializeAsync<T>(stream, _jsonSerializerOptions);
        }
    }
}