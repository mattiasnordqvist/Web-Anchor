using System;
using System.Collections.Generic;
using System.Globalization;

using Castle.Core.Internal;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Formattable
{
    public class FormattableParameterResolver : ParameterListTransformerBase
    {
        public void Resolve(Parameter parameter)
        {
            if (parameter.SourceValue is IFormattable)
            {
                parameter.Value = ((IFormattable)parameter.SourceValue)
                    .ToString(null, CultureInfo.InvariantCulture);
            }
        }

        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            foreach (var parameter in parameters)
            {
                Resolve(parameter);
                yield return parameter;
            }
        }
    }
}