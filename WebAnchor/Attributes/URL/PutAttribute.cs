﻿using System.Net.Http;

namespace WebAnchor.Attributes.URL
{
    public class PutAttribute : HttpAttribute
    {
        public PutAttribute() : this("") { }

        public PutAttribute(string url)
            : base(HttpMethod.Put, url)
        {
            URL = url;
        }
    }
}