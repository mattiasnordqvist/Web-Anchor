using System.Net.Http;

namespace WebAnchor.RequestFactory.HttpAttributes
{
    public class DeleteAttribute : HttpAttribute
    {
        public DeleteAttribute(string url)
            : base(HttpMethod.Delete, url)
        {
            URL = url;
        }
    }
}