using System.Net.Http;

namespace WebAnchor.RequestFactory.HttpAttributes
{
    public class GetAttribute : HttpAttribute
    {
        public GetAttribute(string url)
            : base(HttpMethod.Get, url)
        {
            URL = url;
        }
    }
}