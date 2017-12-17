using System;

namespace WebAnchor.Attributes.Content
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ContentAttribute : Attribute
    {
        public ContentAttribute()
        {
        }
    }
}
