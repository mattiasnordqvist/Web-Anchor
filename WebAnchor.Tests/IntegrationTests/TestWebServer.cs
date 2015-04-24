using Nancy;
using Nancy.ModelBinding;

namespace WebAnchor.Tests.IntegrationTests
{
    public class TestWebServer : NancyModule
    {
        public TestWebServer()
        {
            Get["api/customer/returnnonjson"] = _ => "Hello World!";

            Get["api/customer/{id}"] = _ =>
                {
                    var d = new Customer
                                {
                                    Id = int.Parse(_.id.Value),
                                    Name = "Black Bull"
                                };

                    return Response.AsJson(d);
                };

            Post["api/customer"] = _ =>
                {
                    var d = this.Bind<Customer>();
                    return Response.AsJson(d);
                };

            Post["api/customer/extension"] = _ =>
            {
                var d = this.Bind<Customer>();
                return Response.AsJson(d).WithHeader("location", "api/customer/" + d.Id);
            };

            Get["return"] = _ =>
            {
                var model = this.Bind<DynamicDictionary>();
                return Response.AsJson(model);
            };
        }
    }
}