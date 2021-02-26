using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.Parameters;
using WebAnchor.Attributes.URL;
using Xunit;

namespace WebAnchor.Tests
{
    [BaseLocation("v1")]
    public interface IMyApiV1 : IDisposable
    {
        [Get("/data/{id}")]
        public Task GetData(string? id);

    }

    public class NullableTests
    {

        [Fact]
        public void IfThisCompiles_NullableParameterWorks()
        {
            
            var api = Api.For<IMyApiV1>(new HttpClient());
            Assert.True(true);
        }
    }

}
