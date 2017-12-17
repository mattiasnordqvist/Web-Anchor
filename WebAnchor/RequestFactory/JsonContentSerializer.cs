using System.IO;
using System.Net.Http;
using System.Text;

using Newtonsoft.Json;

namespace WebAnchor.RequestFactory
{
    public class JsonContentSerializer : IContentSerializer
    {
        private readonly JsonSerializer _jsonSerializer;

        public JsonContentSerializer(JsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public HttpContent Serialize(object value, Parameter content)
        {
            if (content == null)
            {
                return null;
            }

            var json = new StringBuilder();
            _jsonSerializer.Serialize(new JsonTextWriter(new StringWriter(json)), value);
            return new StringContent(json.ToString(), Encoding.UTF8, "application/json");
        }
    }
}