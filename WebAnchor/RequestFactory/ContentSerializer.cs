using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

using Castle.Core.Internal;

using Newtonsoft.Json;

namespace WebAnchor.RequestFactory
{
    public class ContentSerializer : IContentSerializer
    {
        private readonly JsonSerializer _jsonSerializer;

        public ContentSerializer(JsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public HttpContent Serialize(Parameter payLoad)
        {
            var value = payLoad.ParameterValue;
            if (payLoad.ParameterInfo.GetAttribute<PayloadAttribute>().Type == PayloadType.FormUrlEncoded)
            {
                var pairs = value as IEnumerable<KeyValuePair<string, string>> ?? value.GetType().GetProperties().ToDictionary(x => x.Name, x => (x.GetGetMethod().Invoke(value, null) == null ? string.Empty : x.GetGetMethod().Invoke(value, null).ToString()));
                return new FormUrlEncodedContent(pairs);
            }
            else
            {
                var json = new StringBuilder();
                _jsonSerializer.Serialize(new JsonTextWriter(new StringWriter(json)), payLoad.ParameterValue);
                return new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            }
        }
    }
}