using System.Net.Http;

namespace WebAnchor.Attributes.URL
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