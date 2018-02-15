using System.Net.Http;
using System.Net.Http.Headers;

namespace WebAnchor
{
    public abstract class ContentPartBase
    {
        protected ContentPartBase(string name, string fileName)
        {
            Name = name;
            FileName = fileName;
        }

        public string Name { get; }
        public string FileName { get; }
        public string ContentType { get; set; }

        public HttpContent CreateContent()
        {
            var content = CreateSpecificContent();

            if (!string.IsNullOrEmpty(ContentType))
            {
                content.Headers.ContentType = MediaTypeHeaderValue.Parse(ContentType);
            }

            if (Name != null)
            {
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = Name,
                    FileName = FileName,
                    FileNameStar = FileName
                };
            }

            return content;
        }

        protected abstract HttpContent CreateSpecificContent();
    }
}