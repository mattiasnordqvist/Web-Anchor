using System.Collections.Generic;

namespace WebAnchor
{
    public class DefaultApiSettings : IApiSettings
    {
        public IApiRequestSettings Request { get; set; } = new DefaultApiRequestSettings();
        public IApiResponeSettings Response { get; set; } = new DefaultApiResponseSettings();
        public IDictionary<string, object> CustomSettings { get; set; } = new Dictionary<string, object>();
    }
}