using System.Net.Http;

namespace WebAnchor.RequestFactory.HttpAttributes
{
    public class PostAttribute : HttpAttribute
    {
        public PostAttribute(string url)
            : base(HttpMethod.Post, url)
        {
            URL = url;
        }
    }
}