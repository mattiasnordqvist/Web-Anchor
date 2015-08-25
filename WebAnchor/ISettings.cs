using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    public interface ISettings
    {
        IHttpRequestFactory GetRequestFactory();
        IHttpResponseParser GetResponseParser();
    }
}