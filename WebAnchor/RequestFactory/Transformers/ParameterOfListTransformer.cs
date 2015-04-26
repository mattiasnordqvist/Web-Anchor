using System.Collections;
using System.Collections.Generic;

namespace WebAnchor.RequestFactory.Transformers
{
    public class ParameterOfListTransformer : IParameterListTransformer
    {
        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            foreach (var parameter in parameters)
            {
                if (parameter.ParameterValue is IEnumerable && parameter.Type.IsGenericType)
                {
                    foreach (var value in (IEnumerable)parameter.ParameterValue)
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
    }
}