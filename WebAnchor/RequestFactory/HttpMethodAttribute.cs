using System;
using System.Net.Http;

namespace WebAnchor.RequestFactory
{
    public abstract class HttpAttribute : Attribute
    {
        protected HttpAttribute(HttpMethod method, string url)
        {
            Method = method;
            URL = url;
        }

        public string URL { get; set; }
        public HttpMethod Method { get; set; }
    }
}