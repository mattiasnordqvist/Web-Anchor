using System;

namespace WebAnchor.Attributes.URL
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class BaseLocationAttribute : Attribute
    {
        public BaseLocationAttribute(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public string BaseUrl { get; set; }
    }
}