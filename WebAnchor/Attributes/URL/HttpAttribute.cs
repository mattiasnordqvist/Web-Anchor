using System;
using System.Net.Http;

namespace WebAnchor.Attributes.URL
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpAttribute : Attribute
    {
        public HttpAttribute(HttpMethod method, string url)
        {
            Method = method;
            URL = url;
        }

        public string URL { get; set; }
        public HttpMethod Method { get; set; }
        public virtual bool IncludeContentInRequest => false;
    }
}