using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace WebAnchor.RequestFactory.Serialization
{
    public class JsonContentSerializer : IContentSerializer
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public JsonContentSerializer(JsonSerializerOptions jsonSerializerOptions)
        {
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public virtual HttpContent Serialize(object value, Parameter content)
        {
            if (content == null)
            {
                return null;
            }


            var json = JsonSerializer.Serialize(value, _jsonSerializerOptions);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}