using System;

namespace WebAnchor.RequestFactory
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