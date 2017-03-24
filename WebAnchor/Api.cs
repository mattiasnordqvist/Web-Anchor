using System;
using System.Net.Http;
using System.Reflection;

using Castle.DynamicProxy;

namespace WebAnchor
{
    public class Api
    {
        private static ProxyGenerator _proxyGenerator;

        protected static ProxyGenerator ProxyGenerator => _proxyGenerator ?? (_proxyGenerator = new ProxyGenerator());

        public static T For<T>(string baseUri, ISettings settings = null) where T : class
        {
            if (!typeof(T).GetTypeInfo().IsInterface)
            {
                throw new WebAnchorException(typeof(T).FullName + " is not an interface and cannot be used with Web Anchor");
            }

            var httpClient = new HttpClient { BaseAddress = new Uri(baseUri) };
            return new ApiFactory(ProxyGenerator).Create<T>(new HttpClientWrapper(httpClient), true, settings);
        }

        public static T For<T>(HttpClient httpClient, ISettings settings = null) where T : class
        {
            return new ApiFactory(ProxyGenerator).Create<T>(new HttpClientWrapper(httpClient), false, settings);
        }
    }
}