using System.Net.Http;

namespace WebAnchor.Attributes.URL
{
    public class GetAttribute : HttpAttribute
    {
        public GetAttribute() : this(string.Empty) { }

        public GetAttribute(string url)
            : base(HttpMethod.Get, url)
        {
            URL = url;
        }
    }
}