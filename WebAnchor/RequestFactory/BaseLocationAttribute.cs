using System;

namespace WebAnchor.RequestFactory
{
    public class BaseLocationAttribute : Attribute
    {
        public BaseLocationAttribute(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public string BaseUrl { get; set; }
    }
}