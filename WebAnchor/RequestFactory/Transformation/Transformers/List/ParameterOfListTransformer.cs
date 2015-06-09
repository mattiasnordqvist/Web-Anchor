using System.Collections;
using System.Collections.Generic;

namespace WebAnchor.RequestFactory.Transformation.Transformers.List
{
    public class ParameterOfListTransformer : ParameterListTransformerBase
    {
        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            foreach (var parameter in parameters)
            {
                if (ParameterIsEnumerable(parameter) && parameter.ParameterType != ParameterType.Content)
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

        protected bool ParameterIsEnumerable(Parameter parameter)
        {
            return parameter.ParameterValue is IEnumerable && (parameter.Type.IsGenericType || parameter.Type.IsArray);
        }
    }
}