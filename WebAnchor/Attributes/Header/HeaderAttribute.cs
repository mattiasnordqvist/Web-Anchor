using System;
using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute;

namespace WebAnchor.Attributes.Header
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