using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Default
{
    public class DictionaryParameterTransformer : IParameterListTransformer
    {
        public IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext)
        {
            foreach (var parameter in parameters)
            {
                if (IsDictionaryParameter(parameter))
                {
                    // Expand dictionary parameter into multiple query parameters
                    foreach (var expandedParameter in ExpandDictionaryParameter(parameter))
                    {
                        yield return expandedParameter;
                    }
                }
                else
                {
                    yield return parameter;
                }
            }
        }

        public void ValidateApi(Type type)
        {
            // No validation needed for dictionary parameters
        }

        private static bool IsDictionaryParameter(Parameter parameter)
        {
            if (parameter.SourceValue == null || parameter.ParameterType != ParameterType.Query)
                return false;

            var sourceType = parameter.SourceValue.GetType();
            
            // Check if it's a Dictionary<string, object> or IDictionary<string, object>
            return sourceType.IsGenericType &&
                   (sourceType.GetGenericTypeDefinition() == typeof(Dictionary<,>) ||
                    sourceType.GetGenericTypeDefinition() == typeof(IDictionary<,>)) &&
                   sourceType.GetGenericArguments()[0] == typeof(string) &&
                   sourceType.GetGenericArguments()[1] == typeof(object);
        }

        private static IEnumerable<Parameter> ExpandDictionaryParameter(Parameter parameter)
        {
            if (parameter.SourceValue is IDictionary<string, object> dictionary)
            {
                foreach (var kvp in dictionary)
                {
                    var expandedParameter = Parameter.CreateQueryParameter(
                        $"{parameter.Name}.{kvp.Key}",
                        new object[] { kvp.Value },
                        parameter.SourceParameterInfo,
                        kvp.Value,
                        parameter);
                    
                    yield return expandedParameter;
                }
            }
        }
    }
}