using System;

namespace WebAnchor.RequestFactory
{
    public class ContentAttribute : Attribute
    {
        public ContentAttribute(ContentType type = ContentType.Json)
        {
            Type = type;
        }

        public ContentType Type { get; set; }
    }
}
