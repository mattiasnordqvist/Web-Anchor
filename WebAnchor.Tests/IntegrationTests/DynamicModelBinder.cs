using System;
using System.Collections.Generic;
using System.Linq;

using Nancy;
using Nancy.ModelBinding;

namespace WebAnchor.Tests.IntegrationTests
{
    public class DynamicModelBinder : IModelBinder
    {
        public object Bind(NancyContext context, Type modelType, object instance, BindingConfig configuration, params string[] blackList)
        {
            var data =
                GetDataFields(context);

            var model =
                DynamicDictionary.Create(data);

            return model;
        }

        public bool CanBind(Type modelType)
        {
            return modelType == typeof(DynamicDictionary);
        }

        private static IDictionary<string, object> GetDataFields(NancyContext context)
        {
            return Merge(new IDictionary<string, string>[]
                             {
                                 ConvertDynamicDictionary(context.Request.Form), 
                                 ConvertDynamicDictionary(context.Request.Query), 
                                 ConvertDynamicDictionary(context.Parameters)
                             });
        }

        private static IDictionary<string, object> Merge(IEnumerable<IDictionary<string, string>> dictionaries)
        {
            var output =
                new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

            foreach (var dictionary in dictionaries.Where(d => d != null))
            {
                foreach (var kvp in dictionary)
                {
                    if (!output.ContainsKey(kvp.Key))
                    {
                        output.Add(kvp.Key, kvp.Value);
                    }
                }
            }

            return output;
        }

        private static IDictionary<string, string> ConvertDynamicDictionary(DynamicDictionary dictionary)
        {
            return dictionary.GetDynamicMemberNames().ToDictionary(
                memberName => memberName,
                memberName => (string)dictionary[memberName]);
        }
    }
}