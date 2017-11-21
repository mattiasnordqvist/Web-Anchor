using System;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Headers.Dynamic
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class HeaderAttribute : ParameterTransformerAttribute
    {
        public HeaderAttribute()
        {
            HeaderName = null;
        }

        public HeaderAttribute(string headerName)
        {
            HeaderName = headerName;
        }

        public string HeaderName { get; set; }

        public override void Apply(Parameter parameter)
        {
            parameter.ParameterType = ParameterType.Header;
            if (HeaderName != null)
            {
                parameter.Name = HeaderName;
            }
        }
    }
}