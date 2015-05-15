using System;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Default
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ContentAttribute : System.Attribute
    {
        public ContentAttribute(ContentType type = ContentType.Json)
        {
            Type = type;
        }

        public ContentType Type { get; set; }
    }
}
