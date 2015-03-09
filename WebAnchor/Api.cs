using System;
using System.Net.Http;

using Castle.DynamicProxy;

using Newtonsoft.Json;

using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    public class Api
    {
        public static T For<T>(string baseUri, IHttpRequestFactory httpRequestFactory = null, IHttpResponseParser httpresponseParser = null, Action<Anchor> configure = null) where T : class
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(baseUri) };
            return For<T>(httpClient, httpRequestFactory, httpresponseParser, configure);
        }

        public static T For<T>(HttpClient httpClient, IHttpRequestFactory httpRequestFactory = null, IHttpResponseParser httpresponseParser = null, Action<Anchor> configure = null) where T : class
        {
            var httpRequestBuilder = httpRequestFactory ?? new HttpRequestFactory(new ContentSerializer());
            var httpResponseParser = httpresponseParser ?? new HttpResponseParser(new JsonContentDeserializer(new JsonSerializer()));
            var configurator = configure ?? (a => { });
            var anchor = new Anchor(httpClient, httpRequestBuilder, httpResponseParser);
            configurator(anchor);
            var api = new ProxyGenerator().CreateInterfaceProxyWithoutTarget<T>(anchor);
            return api;
        }
    }
}