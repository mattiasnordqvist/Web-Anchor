﻿using System.Net.Http;

namespace WebAnchor.RequestFactory
{
    public interface IContentSerializer
    {
        HttpContent Serialize(object value, Parameter content);
    }
}