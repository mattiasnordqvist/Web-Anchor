using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace WebAnchor.RequestFactory.Transformation.Transformers.List
{
    public class ParameterOfListTransformer : IParameterListTransformer
    {
        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            foreach (var parameter in parameters)
            {
                if (ParameterIsEnumerable(parameter) && parameter.ParameterType != ParameterType.Content)
                {
                    foreach (var value in (IEnumerable)parameter.SourceValue)
                    {
                        yield return new Parameter(parameter, value);
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
        }

        protected bool ParameterIsEnumerable(Parameter parameter)
        {
            return parameter.SourceValue is IEnumerable && (parameter.SourceType.GetTypeInfo().IsGenericType || parameter.SourceType.IsArray);
        }
    }
}