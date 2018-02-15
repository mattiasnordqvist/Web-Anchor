using System;
using System.IO;
using System.Net.Http;

namespace WebAnchor
{
    public class StreamContentPart : ContentPartBase
    {
        public StreamContentPart(Stream content)
            : this(content, null, null)
        {
        }

        public StreamContentPart(Stream content, string name)
            : this(content, name, null)
        {
        }

        public StreamContentPart(Stream content, string name, string fileName)
            : base(name, fileName)
        {
            this.Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public Stream Content { get; }

        protected override HttpContent CreateSpecificContent()
        {
            return new StreamContent(this.Content);
        }
    }
}