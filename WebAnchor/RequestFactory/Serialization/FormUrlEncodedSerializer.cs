using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using WebAnchor.Attributes.Content;

namespace WebAnchor.RequestFactory.Serialization
{
    public class FormUrlEncodedSerializer : IContentSerializer
    {
        public virtual HttpContent Serialize(object value, Parameter content)
        {
            if (content == null)
            {
                return null;
            }

            var pairs = value as IEnumerable<KeyValuePair<string, string>> ?? GetKeyValues(value);
            return new FormUrlEncodedContent(pairs);
        }

        protected IEnumerable<KeyValuePair<string, string>> GetKeyValues(object content)
        {
            foreach (var property in content.GetType().GetProperties())
            {
                var name = property.GetCustomAttribute<FormUrlEncodedPropertyAttribute>(true)?.ParameterName ?? property.Name;
                var value = property.GetGetMethod().Invoke(content, null)?.ToString() ?? string.Empty;
                yield return new KeyValuePair<string, string>(name, value);
            }
        }
    }
}
