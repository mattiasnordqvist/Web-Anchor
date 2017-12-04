using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace WebAnchor.RequestFactory
{
    public class FormUrlEncodedSerializer : IContentSerializer
    {
        public HttpContent Serialize(Parameter content)
        {
            if (content == null)
            {
                return null;
            }

            var value = content.Value;
            var pairs = value as IEnumerable<KeyValuePair<string, string>> ?? value.GetType().GetProperties().ToDictionary(x => x.Name, x => (x.GetGetMethod().Invoke(value, null) == null ? string.Empty : x.GetGetMethod().Invoke(value, null).ToString()));
            return new FormUrlEncodedContent(pairs);
        }
    }
}