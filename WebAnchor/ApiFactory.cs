using Castle.DynamicProxy;

namespace WebAnchor
{
    public class ApiFactory
    {
        public ApiFactory()
        {
            Settings = new ApiSettings();
        }

        public ISettings Settings { get; set; }

        public T Create<T>(IHttpClient httpClient, bool shouldDisposeHttpClient, ISettings settings = null) where T : class
        {
            var requestFactory = settings == null ? Settings.GetRequestFactory() : settings.GetRequestFactory();
            requestFactory.ValidateApi(typeof(T));
            var responseParser = settings == null ? Settings.GetResponseParser() : settings.GetResponseParser();
            responseParser.ValidateApi(typeof(T));
            var anchor = new Anchor(httpClient, requestFactory, responseParser, shouldDisposeHttpClient);
            var api = new ProxyGenerator().CreateInterfaceProxyWithoutTarget<T>(anchor);
            return api;
        }
    }
}