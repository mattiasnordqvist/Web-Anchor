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

    [BaseLocation("api/driver")]
    public interface IDriverApi
    {
        [Get("")]
        Task<HttpResponseMessage> GetDrivers(string filter = null);

        [Get("")]
        Task<HttpResponseMessage> GetDrivers2([Prefix("p_")]string filter = null);

        [Get("")]
        Task<HttpResponseMessage> GetDrivers3([Reverse]string filter = null);

        [Get(""), Reverse]
        Task<HttpResponseMessage> GetDrivers4(string filter = null);

        [Get("")]
        Task<HttpResponseMessage> GetDrivers5([Alias("f")]string filter = null);

        [Get("/{resource}")]
        Task<HttpResponseMessage> GetDrivers6(string resource);

        [Get("")]
        Task<HttpResponseMessage> GetDrivers(DateTime from);

        [Get("/{id}")]
        Task<HttpResponseMessage> GetDriver(int id);

        [Get("/{id}")]
        Task<HttpResponseMessage> GetDriver2([Multiply]int id);

        [Get("/{id}")]
        Task<Driver> GetDriver3(int id);

        [Post("")]
        Task<Driver> CreateDriver([Payload(PayloadType.FormUrlEncoded)]Driver driver);

        [Post("")]
        Task<Driver> CreateDriver2([Payload]Driver driver);

        [Get("")]
        Task<HttpResponseMessage> MethodWithListParameter(List<string> names);
        
        [Get("")]
        Task<HttpResponseMessage> MethodWithIntegerListParameter(List<int> values);        

        [Get("/error/404")]
        Task<HttpResponseMessage> Get404();

        [Get("/error/404")]
        Task<Driver> Get404Driver();
        
        [Get("/returnnonjson")]
        Task<Driver> GetAnObject();
    }
}
