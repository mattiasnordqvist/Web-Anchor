using System;
using WebAnchor.RequestFactory;

namespace WebAnchor.Attributes.Content
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ContentAttribute : Attribute
    {
        public ContentAttribute(ContentType type = ContentType.Json)
        {
            Type = type;
        }

        public ContentType Type { get; set; }
    }
}
