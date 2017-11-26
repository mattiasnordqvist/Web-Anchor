using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;

using Newtonsoft.Json;
using WebAnchor.Attributes.Content;
using static WebAnchor.Attributes.Content.ContentAttribute;

namespace WebAnchor.RequestFactory
{
    public class ContentSerializer : IContentSerializer
    {
        private readonly JsonSerializer _jsonSerializer;

        public ContentSerializer(JsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public HttpContent Serialize(Parameter content)
        {
            if (content == null)
            {
                return null;
            }

            var value = content.Value;
            if (content.SourceParameterInfo.GetCustomAttribute<ContentAttribute>().Type == ContentType.FormUrlEncoded)
            {
                var pairs = value as IEnumerable<KeyValuePair<string, string>> ?? value.GetType().GetProperties().ToDictionary(x => x.Name, x => (x.GetGetMethod().Invoke(value, null) == null ? string.Empty : x.GetGetMethod().Invoke(value, null).ToString()));
                return new FormUrlEncodedContent(pairs);
            }
            else
            {
                var json = new StringBuilder();
                _jsonSerializer.Serialize(new JsonTextWriter(new StringWriter(json)), content.Value);
                return new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            }
        }
    }
}