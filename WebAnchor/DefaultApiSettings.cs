using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    public class DefaultApiSettings : IApiSettings
    {
        public IApiRequestSettings Request { get; set; } = new DefaultApiRequestSettings();
        public IApiResponeSettings Response { get; set; } = new DefaultApiResponseSettings();
    }
}