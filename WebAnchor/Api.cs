using System;
using System.Net.Http;

using Castle.DynamicProxy;

namespace WebAnchor
{
    public class Api
    {
        static Api()
        {
            Settings = new ApiSettings();
        }

        public static ISettings Settings { get; set; }

        public static T For<T>(string baseUri, ISettings settings = null) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                throw new WebAnchorException(typeof(T).FullName + " is not an interface and cannot be used with Web Anchor");
            }

            var httpClient = new HttpClient { BaseAddress = new Uri(baseUri) };
            return For<T>(httpClient, settings);
        }

        public static T For<T>(HttpClient httpClient, ISettings settings = null) where T : class
        {
            var requestFactory = settings == null ? Settings.GetRequestFactory() : settings.GetRequestFactory();
            var responseParser = settings == null ? Settings.GetResponseParser() : settings.GetResponseParser();
            responseParser.ValidateApi(typeof(T));
            var anchor = new Anchor(httpClient, requestFactory, responseParser);
            var api = new ProxyGenerator().CreateInterfaceProxyWithoutTarget<T>(anchor);
            return api;
        }
    }
}