using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WebAnchor.RequestFactory.Resolvers
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
                    if (s.Value == null || s.Value.GetType().IsPrimitive || s.Value is string)
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

        private static void Add(string name, object value, Dictionary<string, object> dictionary)
        {
            if (value == null || value.GetType().IsPrimitive || value is string)
            {
                dictionary.Add(name, value);
            }
            else if (value is IEnumerable<object>)
            {
                dictionary.Add(name, ((IEnumerable<object>)value).Select(
                    x =>
                        {
                            if (x.GetType().IsPrimitive || x is string)
                            {
                                return x;
                            }
                            return x.ToDictionary();
                        }).ToList());
            }
            else
            {
                dictionary.Add(name, value.ToDictionary());
            }   
        }
    }
}