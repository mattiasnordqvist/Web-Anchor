using System.Collections.Generic;
using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    public interface IApiSettings
    {
        Dictionary<string, object> Data { get; set; }
        IApiRequestSettings Request { get; set; }
        IApiResponeSettings Response { get; set; }
    }
}