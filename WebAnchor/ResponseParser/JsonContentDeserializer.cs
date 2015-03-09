using System.IO;
using System.Net.Http;

using Newtonsoft.Json;

namespace WebAnchor.ResponseParser
{
    public class JsonContentDeserializer : IContentDeserializer
    {
        private readonly JsonSerializer _jsonSerializer;

        public JsonContentDeserializer(JsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public T Deserialize<T>(StreamReader streamReader, HttpResponseMessage message)
        {
            using (var jr = new JsonTextReader(streamReader))
            {
                var u = _jsonSerializer.Deserialize<T>(jr);
                return u;
            }
        }
    }
}