using System.Net.Http;

namespace WebAnchor
{
    public class ByteArrayContentPart : ContentPartBase
    {
        public ByteArrayContentPart(byte[] content)
            : this(content, null, null)
        {
        }

        public ByteArrayContentPart(byte[] content, string name)
            : this(content, name, null)
        {
        }

        public ByteArrayContentPart(byte[] content, string name, string fileName)
            : base(name, fileName)
        {
            this.Content = content;
        }

        public byte[] Content { get; }

        protected override HttpContent CreateSpecificContent()
        {
            return new ByteArrayContent(this.Content);
        }
    }
}