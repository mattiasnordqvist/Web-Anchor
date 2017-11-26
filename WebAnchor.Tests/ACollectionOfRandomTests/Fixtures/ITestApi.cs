using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;
using WebAnchor.RequestFactory.Transformation.Transformers.Alias;
using WebAnchor.Tests.RequestFactory.Transformation.Transformers.Attribute.Fixtures;

namespace WebAnchor.Tests.ACollectionOfRandomTests.Fixtures
{
    [BaseLocation("api/{version}")]
    public interface IBaseLocationSubstitution
    {
        [Get("")]
        Task<HttpResponseMessage> Get();
    }

    [BaseLocation("api/reversed")]
    [Reverse]
    public interface IReversedApi
    {
        [Get("")]
        Task<HttpResponseMessage> GetSomething(string filter = null);
    }

    [BaseLocation("api/customer")]
    public interface ITestApi
    {
        [Post("")]
        Task<HttpResponseMessage> PostWithoutPayload();

        [Get("")]
        Task<HttpResponseMessage> GetCustomers(string filter = null);

        [Get(""), Reverse]
        Task<HttpResponseMessage> GetCustomers4(string filter = null);

        [Get("")]
        Task<HttpResponseMessage> GetCustomers5([Alias("f")]string filter = null);

        [Get("/{resource}")]
        Task<HttpResponseMessage> GetCustomers6(string resource);

        [Get("?extraParam=7")]
        Task<HttpResponseMessage> GetCustomers7(string filter = null);

        [Get("")]
        Task<HttpResponseMessage> GetCustomers(DateTime from);

        [Get("/{id}")]
        Task<HttpResponseMessage> GetCustomer(int id);

        [Get("/{id}")]
        Task<Customer> GetCustomer3(int id);

        [Get("")]
        Task<HttpResponseMessage> MethodWithListParameter(List<string> names);
        
        [Get("")]
        Task<HttpResponseMessage> MethodWithIntegerListParameter(List<int> values);

        [Get("")]
        Task<HttpResponseMessage> MethodWithIntegerArrayParameter(int[] values);

        [Get("/error/404")]
        Task<HttpResponseMessage> Get404();

        [Get("/error/404")]
        Task<Customer> Get404Customer();
        
        [Get("/returnnonjson")]
        Task<Customer> GetAnObject();
    }
}
