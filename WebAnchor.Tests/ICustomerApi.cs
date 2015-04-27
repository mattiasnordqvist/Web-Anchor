using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.HttpAttributes;
using WebAnchor.RequestFactory.Resolvers;

namespace WebAnchor.Tests
{
    [BaseLocation("api/reversed")]
    [Reverse]
    public interface IReversedApi
    {
        [Get("")]
        Task<HttpResponseMessage> GetSomething(string filter = null);
    }

    [BaseLocation("api/customer")]
    public interface ICustomerApi
    {
        [Get("")]
        Task<HttpResponseMessage> GetCustomers(string filter = null);

        [Get("")]
        Task<HttpResponseMessage> GetCustomers2([Prefix("p_")]string filter = null);

        [Get("")]
        Task<HttpResponseMessage> GetCustomers3([Reverse]string filter = null);

        [Get(""), Reverse]
        Task<HttpResponseMessage> GetCustomers4(string filter = null);

        [Get("")]
        Task<HttpResponseMessage> GetCustomers5([Alias("f")]string filter = null);

        [Get("/{resource}")]
        Task<HttpResponseMessage> GetCustomers6(string resource);

        [Get("")]
        Task<HttpResponseMessage> GetCustomers(DateTime from);

        [Get("/{id}")]
        Task<HttpResponseMessage> GetCustomer(int id);

        [Get("/{id}")]
        Task<HttpResponseMessage> GetCustomer2([Multiply]int id);

        [Get("/{id}")]
        Task<Customer> GetCustomer3(int id);

        [Post("")]
        Task<Customer> CreateDriver([Content(ContentType.FormUrlEncoded)]Customer customer);

        [Post("")]
        Task<Customer> CreateDriver2([Content]Customer customer);

        [Post("/deep")]
        Task<DeepObject> CreateDeepobject([Content]DeepObject deepobject);

        [Post("/extension")]
        Task<CustomerWithLocation> CreateDriverWithLocation([Content]Customer customer);

        [Get("")]
        Task<HttpResponseMessage> MethodWithListParameter(List<string> names);
        
        [Get("")]
        Task<HttpResponseMessage> MethodWithIntegerListParameter(List<int> values);        

        [Get("/error/404")]
        Task<HttpResponseMessage> Get404();

        [Get("/error/404")]
        Task<Customer> Get404Driver();
        
        [Get("/returnnonjson")]
        Task<Customer> GetAnObject();
    }
}
