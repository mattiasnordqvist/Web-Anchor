using System.Reflection;

using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;
using System.Linq;
using System;

namespace WebAnchor
{

    public class ApiFactory
    {
        public T Create<T>(IHttpClient httpClient, bool shouldDisposeHttpClient, IApiSettings settings) where T : class
        {
            var requestFactory = new HttpRequestFactory(settings);
            requestFactory.ValidateApi(typeof(T));
            var responseHandlersList = new HttpResponseHandlersList(settings);
            responseHandlersList.ValidateApi(typeof(T));

            // This code can't find (or probably actually create) generic interface implementations.
            var types = Assembly.GetAssembly(typeof(T)).GetTypes();
            var implementor = types
                .First(x => x.IsClass && typeof(T).IsAssignableFrom(x));
            var anchor = new Anchor(httpClient, requestFactory, responseHandlersList, shouldDisposeHttpClient);

            var api = (T)Activator.CreateInstance(implementor, anchor);

            return api;
        }
    }
}