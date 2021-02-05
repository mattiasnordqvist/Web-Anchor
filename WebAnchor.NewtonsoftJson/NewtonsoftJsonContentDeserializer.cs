using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace WebAnchor.ResponseParser
{
    public class NewtonsoftJsonContentDeserializer : IContentDeserializer
    {
        private readonly JsonSerializer _jsonSerializer;

        public NewtonsoftJsonContentDeserializer(JsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public virtual Task<T> Deserialize<T>(Stream stream, HttpResponseMessage message)
        {
            using (var streamReader = new StreamReader(stream))
            using (var jr = new JsonTextReader(streamReader))
            {
                var u = _jsonSerializer.Deserialize<T>(jr);
                return Task.FromResult(u);
            }
        }
    }
}