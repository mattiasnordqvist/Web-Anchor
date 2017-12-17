using System.Net.Http;

namespace WebAnchor.Attributes.URL
{
    public class DeleteAttribute : HttpAttribute
    {
        public DeleteAttribute() : this("") { }

        public DeleteAttribute(string url)
            : base(HttpMethod.Delete, url)
        {
            URL = url;
        }
    }
}