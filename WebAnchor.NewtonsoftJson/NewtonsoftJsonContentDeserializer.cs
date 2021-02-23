using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.ResponseParser;

namespace WebAnchor.NewtonsoftJson
{
    public class NewtonsoftJsonContentDeserializer : IContentDeserializer
    {
        private readonly JsonSerializer _jsonSerializer;

        public NewtonsoftJsonContentDeserializer(JsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public virtual async ValueTask<T> Deserialize<T>(Stream stream, HttpResponseMessage message)
        {
            using (var streamReader = new StreamReader(stream))
            using (var jr = new JsonTextReader(streamReader))
            {
                var u = _jsonSerializer.Deserialize<T>(jr);
                return await Task.FromResult(u);
            }
        }
    }
}
