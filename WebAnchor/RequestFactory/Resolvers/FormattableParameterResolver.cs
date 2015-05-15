using System;
using System.Collections.Generic;
using System.Globalization;

using Castle.Core.Internal;

namespace WebAnchor.RequestFactory.Resolvers
{
    public class FormattableParameterResolver : IParameterListTransformer
    {
        public void Resolve(Parameter parameter)
        {
            if (parameter.ParameterValue is IFormattable)
            {
                parameter.Value = ((IFormattable)parameter.ParameterValue)
                    .ToString(null, CultureInfo.InvariantCulture);
            }
        }

        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            parameters.ForEach(Resolve);
            return parameters;
        }
    }
}