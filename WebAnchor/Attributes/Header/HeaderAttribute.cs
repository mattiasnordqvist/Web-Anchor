using System;
using WebAnchor.RequestFactory;

namespace WebAnchor.Attributes.Header
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class HeaderAttribute : Attribute
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

        public void Apply(Parameter p)
        {
            if (HeaderName != null)
            {
                p.Name = HeaderName;
            }
        }
    }
}