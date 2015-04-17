using Newtonsoft.Json;

using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    public class ApiSettings : ISettings
    {
        public ApiSettings()
        {
            RequestFactory = new HttpRequestFactory(new ContentSerializer(new JsonSerializer()));
            ResponseParser = new HttpResponseParser(new JsonContentDeserializer(new JsonSerializer()));
        }

        public IHttpRequestFactory RequestFactory { get; set; }
        public IHttpResponseParser ResponseParser { get; set; }
    }
}