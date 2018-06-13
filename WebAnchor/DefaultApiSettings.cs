using System.Collections.Generic;
using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    public class DefaultApiSettings : IApiSettings
    {
        public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
        public IApiRequestSettings Request { get; set; } = new DefaultApiRequestSettings();
        public IApiResponeSettings Response { get; set; } = new DefaultApiResponseSettings();
    }
}