using System;
using System.Net.Http;

namespace WebAnchor
{
    public class Api
    {
        public static T For<T>(string baseUri, ISettings settings = null) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                throw new WebAnchorException(typeof(T).FullName + " is not an interface and cannot be used with Web Anchor");
            }

            var httpClient = new HttpClient { BaseAddress = new Uri(baseUri) };
            return new ApiFactory().Create<T>(new HttpClientWrapper(httpClient), true, settings);
        }

        public static T For<T>(HttpClient httpClient, ISettings settings = null) where T : class
        {
            return new ApiFactory().Create<T>(new HttpClientWrapper(httpClient), false, settings);
        }
    }
}