using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Default
{
    public static class ObjectToDictionaryHelper
    {
        public static IDictionary<string, object> ToDictionary(this object source)
        {
            var dictionary = new Dictionary<string, object>();
            if (source as IEnumerable<KeyValuePair<string, object>> != null)
            {
                foreach (var s in (IEnumerable<KeyValuePair<string, object>>)source)
                {
                    if (IsPrimitive(s.Value))
                    {
                        dictionary.Add(s.Key, s.Value);
                    }
                    else
                    {
                        Add(s.Key, s.Value, dictionary);
                    }
                }
            }
            else
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                {
                    var value = property.GetValue(source);
                    var name = property.Name;
                    Add(name, value, dictionary);
                }
            }

            return dictionary;
        }

        private static bool IsPrimitive(object value)
        {
            return value == null || value.GetType().GetTypeInfo().IsPrimitive || value is string;
        }

        private static void Add(string name, object value, Dictionary<string, object> dictionary)
        {
            if (IsPrimitive(value))
            {
                dictionary.Add(name, value);
            }
            else if (value is IEnumerable<object>)
            {
                dictionary.Add(name, ((IEnumerable<object>)value).Select(x => IsPrimitive(x) ? x : x.ToDictionary()).ToList());
            }
            else if (value is IEnumerable)
            {
                dictionary.Add(name, value);
            }
            else
            {
                dictionary.Add(name, value.ToDictionary());
            }   
        }
    }
}