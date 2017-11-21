using System;

namespace WebAnchor
{
    /// <summary>
    /// Intended for Web Anchor api validation errors
    /// </summary>
    public class WebAnchorException : Exception
    {
        public WebAnchorException(string message) : base(message)
        {
        }
    }
}