using Nancy;
using Nancy.ModelBinding;

namespace WebAnchor.Tests.IntegrationTests
{
    public class TestWebServer : NancyModule
    {
        public TestWebServer()
        {
            Get["api/driver/returnnonjson"] = _ => "Hello World!";

            Get["api/driver/{id}"] = _ =>
                {
                    var d = new Driver
                                {
                                    Id = int.Parse(_.id.Value),
                                    Name = "Black Bull"
                                };

                    return Response.AsJson(d);
                };

            Post["api/driver"] = _ =>
                {
                    var d = this.Bind<Driver>();
                    return Response.AsJson(d);
                };

            Post["api/driver/extension"] = _ =>
            {
                var d = this.Bind<Driver>();
                return Response.AsJson(d).WithHeader("location", "api/driver/" + d.Id);
            };

            Get["return"] = _ =>
            {
                var model = this.Bind<DynamicDictionary>();
                return Response.AsJson(model);
            };
        }
    }
}