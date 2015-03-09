using System;
using System.Globalization;

namespace WebAnchor.RequestFactory.Resolvers
{
    public class FormattableParameterResolver : IParameterResolver
    {
        public void Resolve(Parameter parameter)
        {
            if (parameter.ParameterValue is IFormattable)
            {
                parameter.Value = ((IFormattable)parameter.ParameterValue)
                    .ToString(null, CultureInfo.InvariantCulture);
            }
        }
    }
}