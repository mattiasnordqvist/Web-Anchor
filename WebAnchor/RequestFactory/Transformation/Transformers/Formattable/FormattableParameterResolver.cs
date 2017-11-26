using System;
using System.Collections.Generic;
using System.Globalization;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Formattable
{
    public class FormattableParameterResolver : IParameterListTransformer
    {
        public void Resolve(Parameter parameter)
        {
            if (parameter.SourceValue is IFormattable)
            {
                parameter.Value = ((IFormattable)parameter.SourceValue)
                    .ToString(null, CultureInfo.InvariantCulture);
            }
        }

        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            foreach (var parameter in parameters)
            {
                Resolve(parameter);
                yield return parameter;
            }
        }

        public void ValidateApi(Type type)
        {
        }
    }
}