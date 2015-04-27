using System;
using System.Collections.Generic;
using System.ComponentModel;

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
                    Add(s.Key, s.Value, dictionary);
                }
            }
            else
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                {
                    AddPropertyToDictionary(property, source, dictionary);
                }

            }

            return dictionary;
        }

        private static void AddPropertyToDictionary(PropertyDescriptor property, object source, Dictionary<string, object> dictionary)
        {
            var value = property.GetValue(source);
            var name = property.Name;
            Add(name, value, dictionary);
        }

        private static void Add(string name, object value, Dictionary<string, object> dictionary)
        {
            if (value.GetType().IsPrimitive || value is string)
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