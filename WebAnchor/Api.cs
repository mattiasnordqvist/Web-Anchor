using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAnchor
{
    /// <summary>
    /// Web Anchor main class. Use the static methods of this class to create an instance of your api.
    /// </summary>
    public class Api
    {
        /// <summary>
        /// Creates an api of type T. T must be an interface that looks like a web anchor api.
        /// This method will create a httpClient on the fly. If T extends IDisposable, that HttpClient will 
        /// be disposed when your api is disposed.
        /// </summary>
        /// <typeparam name="T">An interface for your Api.</typeparam>
        /// <param name="baseUri">A base URI that will be used for ALL httprequests created by this web anchor api. This URI will never be modified by any web anchor magic.</param>
        /// <param name="settings">Contains everything about how your http requests are constructed and parsed. If not passed, ApiSettings will be used.</param>
        /// <returns></returns>
        public static T For<T>(string baseUri, IApiSettings settings = null) where T : class
        {
            if (!typeof(T).GetTypeInfo().IsInterface)
            {
                throw new WebAnchorException(typeof(T).FullName + " is not an interface and cannot be used with Web Anchor");
            }

            var httpClient = new HttpClient { BaseAddress = new Uri(baseUri) };
            return new ApiFactory().Create<T>(new HttpClientWrapper(httpClient), true, settings ?? new DefaultApiSettings());
        }

        /// <summary>
        /// Creates an api of type T. T must be an interface that looks like a web anchor api.
        /// This method will create a httpClient on the fly. If T extends IDisposable, that HttpClient will 
        /// be disposed when your api is disposed.
        /// </summary>
        /// <typeparam name="T">An interface for your Api.</typeparam>
        /// <param name="baseUri">A base URI that will be used for ALL httprequests created by this web anchor api. This URI will never be modified by any web anchor magic.</param>
        /// <param name="configure">Offers you a convenient option to configure a default ApiSettings</param>
        /// <returns></returns>
        public static T For<T>(string baseUri, Action<DefaultApiSettings> configure) where T : class
        {
            var settings = new DefaultApiSettings();
            configure?.Invoke(settings);
            return For<T>(baseUri, settings);
        }

        /// <summary>
        /// Creates an api of type T. T must be an interface that looks like a web anchor api.
        /// With this method you can supply your own httpClient. You will be responsible to dispose this HttpClient yourself.
        /// </summary>
        /// <typeparam name="T">An interface for your Api.</typeparam>
        /// <param name="httpClient">A HttpClient that you can modify the way you like. You must also handle disposing of this HttpClient yourself.</param>
        /// <param name="settings">Contains everything about how your http requests are constructed and parsed. If not passed, ApiSettings will be used.</param>
        /// <returns></returns>
        public static T For<T>(HttpClient httpClient, IApiSettings settings = null) where T : class
        {
            return new ApiFactory().Create<T>(new HttpClientWrapper(httpClient), false, settings ?? new DefaultApiSettings());
        }

        /// <summary>
        /// Creates an api of type T. T must be an interface that looks like a web anchor api.
        /// With this method you can supply your own httpClient. You will be responsible to dispose this HttpClient yourself.
        /// </summary>
        /// <typeparam name="T">An interface for your Api.</typeparam>
        /// <param name="httpClient">A HttpClient that you can modify the way you like. You must also handle disposing of this HttpClient yourself.</param>
        /// <param name="configure">Offers you a convenient option to configure a default ApiSettings</param>
        /// <returns></returns>
        public static T For<T>(HttpClient httpClient, Action<DefaultApiSettings> configure) where T : class
        {
            var settings = new DefaultApiSettings();
            configure?.Invoke(settings);
            return For<T>(httpClient, settings);
        }

        /// <summary>
        /// Creates an api of type T. T must be an interface that looks like a web anchor api.
        /// With this method you can supply your own httpClient. You will be responsible to dispose this HttpClient yourself.
        /// </summary>
        /// <typeparam name="T">An interface for your Api.</typeparam>
        /// <param name="httpClient">A HttpClient that you can modify the way you like. You must also handle disposing of this HttpClient yourself.</param>
        /// <param name="settings">Contains everything about how your http requests are constructed and parsed. If not passed, ApiSettings will be used.</param>
        /// <returns></returns>
        public static T For<T>(IHttpClient httpClient, IApiSettings settings = null) where T : class
        {
            return new ApiFactory().Create<T>(httpClient, false, settings ?? new DefaultApiSettings());
        }

        /// <summary>
        /// Creates an api of type T. T must be an interface that looks like a web anchor api.
        /// With this method you can supply your own httpClient. You will be responsible to dispose this HttpClient yourself.
        /// </summary>
        /// <typeparam name="T">An interface for your Api.</typeparam>
        /// <param name="httpClient">A HttpClient that you can modify the way you like. You must also handle disposing of this HttpClient yourself.</param>
        /// <param name="configure">Offers you a convenient option to configure a default ApiSettings</param>
        /// <returns></returns>
        public static T For<T>(IHttpClient httpClient, Action<DefaultApiSettings> configure) where T : class
        {
            var settings = new DefaultApiSettings();
            configure?.Invoke(settings);
            return For<T>(httpClient, settings);
        }
    }
}