using System;
using System.Net.Http;

namespace WebAnchor
{
    public class StringContentPart : ContentPartBase
    {
        public StringContentPart(string content)
            : this(content, null, null)
        {
        }

        public StringContentPart(string content, string name)
            : this(content, name, null)
        {
        }

        public StringContentPart(string content, string name, string fileName)
            : base(name, fileName)
        {
            this.Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public string Content { get; }

        protected override HttpContent CreateSpecificContent()
        {
            return new StringContent(this.Content);
        }
    }
}