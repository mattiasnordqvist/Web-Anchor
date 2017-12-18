using System;
using System.Globalization;

namespace WebAnchor.RequestFactory.ValueFormatting
{
    public class DefaultParameterValueFormatter : IParameterValueFormatter
    {
        public bool FormatFormattables = true;

        public CultureInfo CultureInfo = CultureInfo.InvariantCulture;

        public string Format(object value, Parameter parameter)
        {
            return FormatFormattables && value is IFormattable ? ((IFormattable)value).ToString(null, CultureInfo) : value.ToString();
        }
    }
}