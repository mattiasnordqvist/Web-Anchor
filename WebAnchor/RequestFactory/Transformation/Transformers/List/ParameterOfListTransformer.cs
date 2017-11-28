using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace WebAnchor.RequestFactory.Transformation.Transformers.List
{
    public class ParameterOfListTransformer : IParameterListTransformer
    {
        public IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext)
        {
            foreach (var parameter in parameters)
            {
                if (ParameterIsEnumerable(parameter) && (parameter.ParameterType == ParameterType.Query || parameter.ParameterType == ParameterType.Header))
                {
                    foreach (var value in (IEnumerable)parameter.Value)
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
            return parameter.Value is IEnumerable && (parameter.Value.GetType().GetTypeInfo().IsGenericType || parameter.Value.GetType().IsArray);
        }
    }
}