using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    public interface IApiSettings
    {
        IApiRequestSettings Request { get; set; }
        IApiResponeSettings Response { get; set; }
    }
}