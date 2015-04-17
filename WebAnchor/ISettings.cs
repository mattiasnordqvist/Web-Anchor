using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    public interface ISettings
    {
        IHttpRequestFactory RequestFactory { get; set; }
        IHttpResponseParser ResponseParser { get; set; }
    }
}