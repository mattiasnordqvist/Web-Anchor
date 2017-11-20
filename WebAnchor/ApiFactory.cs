using Castle.DynamicProxy;

namespace WebAnchor
{
    /// <summary>
    /// Not intended to be used by WebAnchor consumers. Use Api instead.
    /// </summary>
    public class ApiFactory
    {
        private static ProxyGenerator _proxyGenerator;
        protected static ProxyGenerator ProxyGenerator => _proxyGenerator ?? (_proxyGenerator = new ProxyGenerator());

        public T Create<T>(IHttpClient httpClient, bool shouldDisposeHttpClient, ISettings settings) where T : class
        {
            var requestFactory = settings.GetRequestFactory();
            requestFactory.ValidateApi(typeof(T));
            var responseParser = settings.GetResponseParser();
            responseParser.ValidateApi(typeof(T));
            var anchor = new Anchor(httpClient, requestFactory, responseParser, shouldDisposeHttpClient);
            var api = ProxyGenerator.CreateInterfaceProxyWithoutTarget<T>(anchor);
            return api;
        }
    }
}