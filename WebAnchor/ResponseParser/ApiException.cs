using System;
using System.Net.Http;

namespace WebAnchor.ResponseParser
{
    public class ApiException : Exception
    {
        public ApiException(HttpResponseMessage httpResponseMessage)
            : base("Response status does not indicate success (" + httpResponseMessage.StatusCode + ")")
        {
            HttpResponseMessage = httpResponseMessage;
        }

        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}