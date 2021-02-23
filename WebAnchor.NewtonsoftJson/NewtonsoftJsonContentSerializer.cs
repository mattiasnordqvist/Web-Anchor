using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Serialization;

namespace WebAnchor.NewtonsoftJson
{
    public class NewtonsoftJsonContentSerializer : IContentSerializer
    {
        private readonly JsonSerializer _jsonSerializer;

        public NewtonsoftJsonContentSerializer(JsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public virtual HttpContent Serialize(object value, Parameter content)
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
