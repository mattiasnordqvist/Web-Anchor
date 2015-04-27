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
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
            {
                AddPropertyToDictionary(property, source, dictionary);
            }

            return dictionary;
        }

        private static void AddPropertyToDictionary(PropertyDescriptor property, object source, Dictionary<string, object> dictionary)
        {
            var value = property.GetValue(source);

            if (value.GetType().IsPrimitive || value is string)
            {
                dictionary.Add(property.Name, value);
            }
            else
            {
                dictionary.Add(property.Name, value.ToDictionary());
            }
        }
    }
}