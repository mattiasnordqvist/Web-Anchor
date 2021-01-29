using System.Reflection;

using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;
using System.Linq;
using System;

namespace WebAnchor
{

    public class ApiFactory2
    {
        public T Create<T>(IHttpClient httpClient, bool shouldDisposeHttpClient, IApiSettings settings) where T : class
        {
            var requestFactory = new HttpRequestFactory(settings);
            requestFactory.ValidateApi(typeof(T));
            var responseHandlersList = new HttpResponseHandlersList(settings);
            responseHandlersList.ValidateApi(typeof(T));

            var types = Assembly.GetEntryAssembly()
                .GetTypes();
            var implementor = types
                .First(x => x.IsClass && typeof(T).IsAssignableFrom(x));
            var anchor = new Anchor(httpClient, requestFactory, responseHandlersList, shouldDisposeHttpClient);

            var api = (T)Activator.CreateInstance(implementor, anchor);

            return api;
        }
    }

    //public class ApiImplementation : IApi
    //{
    //    private readonly Anchor2 anchor;

    //    public ApiImplementation(Anchor2 anchor)
    //    {
    //        this.anchor = anchor;
    //    }

    //    public async Task<T> GetCustomer(int id)
    //    {
    //        return await anchor.Intercept<T>(metadata);
    //    }

    //    public void Dispose()
    //    {
    //        this.anchor.Dispose();

    //    }
    //}
}