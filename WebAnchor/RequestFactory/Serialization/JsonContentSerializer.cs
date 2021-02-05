using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace WebAnchor.RequestFactory.Serialization
{
    public class JsonContentSerializer : IContentSerializer
    {
        private readonly JsonSerializerOptions _options;

        public JsonContentSerializer(JsonSerializerOptions options = null)
        {
            _options = options;
        }

        public virtual HttpContent Serialize(object value, Parameter content)
        {
            if (content == null)
            {
                return null;
            }

            var json = JsonSerializer.Serialize(value, _options);
            return new StringContent(json.ToString(), Encoding.UTF8, "application/json");
        }
    }
}