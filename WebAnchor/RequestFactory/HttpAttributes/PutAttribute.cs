using System.Net.Http;

namespace WebAnchor.RequestFactory.HttpAttributes
{
    public class PutAttribute : HttpAttribute
    {
        public PutAttribute(string url)
            : base(HttpMethod.Put, url)
        {
            URL = url;
        }
    }
}