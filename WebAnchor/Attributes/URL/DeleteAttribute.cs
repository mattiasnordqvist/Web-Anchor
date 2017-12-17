using System.Net.Http;

namespace WebAnchor.Attributes.URL
{
    public class DeleteAttribute : HttpAttribute
    {
        public DeleteAttribute() : this(string.Empty) { }

        public DeleteAttribute(string url)
            : base(HttpMethod.Delete, url)
        {
            URL = url;
        }
    }
}