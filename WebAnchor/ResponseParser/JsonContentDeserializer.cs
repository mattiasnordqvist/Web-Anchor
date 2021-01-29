using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
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

        public virtual T Deserialize<T>(Stream stream, HttpResponseMessage message)
        {
            using (var streamReader = new StreamReader(stream))
            using (var jr = new JsonTextReader(streamReader))
            {
                var u = _jsonSerializer.Deserialize<T>(jr);
                return u;
            }
        }
    }
}