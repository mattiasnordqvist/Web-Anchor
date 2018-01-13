using Castle.DynamicProxy;
using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    /// <summary>
    /// Not intended to be used by WebAnchor consumers. Use Api instead.
    /// </summary>
    public class ApiFactory
    {
        private static ProxyGenerator _proxyGenerator;
        protected static ProxyGenerator ProxyGenerator => _proxyGenerator ?? (_proxyGenerator = new ProxyGenerator());

        public T Create<T>(IHttpClient httpClient, bool shouldDisposeHttpClient, IApiSettings settings) where T : class
        {
            var requestFactory = new HttpRequestFactory(settings);
            requestFactory.ValidateApi(typeof(T));
            var responseHandlersList = new HttpResponseHandlersList(settings);
            responseHandlersList.ValidateApi(typeof(T));
            var anchor = new Anchor(httpClient, requestFactory, responseHandlersList, shouldDisposeHttpClient);
            var api = ProxyGenerator.CreateInterfaceProxyWithoutTarget<T>(anchor);
            return api;
        }
    }
}