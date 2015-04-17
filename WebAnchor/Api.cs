using System;
using System.Net.Http;

using Castle.DynamicProxy;

using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    public class Api
    {
        static Api()
        {
            Settings = new ApiSettings();
        }

        public static ISettings Settings { get; set; }

        public static T For<T>(string baseUri, IHttpRequestFactory httpRequestFactory = null, IHttpResponseParser httpResponseParser = null, Action<Anchor> configure = null) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                throw new WebAnchorException(typeof(T).FullName + " is not an interface and cannot be used with Web Anchor");
            }

            var httpClient = new HttpClient { BaseAddress = new Uri(baseUri) };
            return For<T>(httpClient, httpRequestFactory, httpResponseParser, configure);
        }

        public static T For<T>(HttpClient httpClient, IHttpRequestFactory httpRequestFactory = null, IHttpResponseParser httpResponseParser = null, Action<Anchor> configure = null) where T : class
        {
            var requestFactory = httpRequestFactory ?? Settings.RequestFactory;
            var responseParser = httpResponseParser ?? Settings.ResponseParser;
            var configurator = configure ?? (a => { });
            var anchor = new Anchor(httpClient, requestFactory, responseParser);
            configurator(anchor);
            var api = new ProxyGenerator().CreateInterfaceProxyWithoutTarget<T>(anchor);
            return api;
        }
    }
}