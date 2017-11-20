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

        protected bool ParameterIsEnumerable(Parameter parameter)
        {
            return parameter.SourceValue is IEnumerable && (parameter.SourceType.IsGenericType || parameter.SourceType.IsArray);
        }
    }
}