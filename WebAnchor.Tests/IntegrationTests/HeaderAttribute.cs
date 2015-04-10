using System;

namespace WebAnchor.Tests.IntegrationTests
{
    public class HeaderAttribute : Attribute
    {
        public HeaderAttribute(string header)
        {
            Header = header;
        }

        public string Header { get; private set; }
    }
}