using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.Parameters;
using WebAnchor.Attributes.URL;
using WebAnchor.RequestFactory.ValueFormatting;
using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.ACollectionOfRandomTests
{
    public class AsListTests : WebAnchorTest
    {
        [BaseLocation("api")]
        public interface IMyApi
        {
            [Get("")]
            Task<HttpResponseMessage> Get(string[] values);

            [Get("")]
            Task<HttpResponseMessage> Get2(string[] values);

            [Get("")]
            Task<HttpResponseMessage> Get3([Alias("v")] string[] values);

            [Get("")]
            Task<HttpResponseMessage> Get4(decimal[] values);

            [Get("")]
            Task<HttpResponseMessage> Get5(decimal[] values);

            [Get("")]
            Task<HttpResponseMessage> Get6([Alias("v")] string[] values);
        }

        [Fact]
        public void TestNormalList()
        {
            TestTheRequest<IMyApi>(
               api => api.Get(new string[] { "abc", "def", "ghi" }),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api?values=abc&values=def&values=ghi", assertMe.RequestUri.ToString());
               });
        }

        [Fact]
        public void TestNormalListWithAlias()
        {
            TestTheRequest<IMyApi>(
               api => api.Get6(new string[] { "abc", "def", "ghi" }),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api?v=abc&v=def&v=ghi", assertMe.RequestUri.ToString());
               });
        }

        [Fact]
        public void TestCommaSeparatedList()
        {
            TestTheRequest<IMyApi>(
               api => api.Get2(new string[] { "abc", "def", "ghi" }),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api?values=abc,def,ghi", assertMe.RequestUri.ToString());
               },
               x => x.Request.QueryParameterListStrategy = new DelimitedQueryParamaterListStrategy());
        }

        [Fact]
        public void TestAliasedCommaSeparatedList()
        {
            TestTheRequest<IMyApi>(
               api => api.Get3(new string[] { "abc", "def", "ghi" }),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api?v=abc,def,ghi", assertMe.RequestUri.ToString());
               },
               x => x.Request.QueryParameterListStrategy = new DelimitedQueryParamaterListStrategy());
        }

        [Fact]
        public void TestFormattableList()
        {
            TestTheRequest<IMyApi>(
               api => api.Get4(new decimal[] { 1.2m, 2.4m }),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api?values=1.2,2.4", assertMe.RequestUri.ToString());
               },
               x => x.Request.QueryParameterListStrategy = new DelimitedQueryParamaterListStrategy());
        }

        [Fact]
        public void TestFormattableCommaSeparatedList()
        {
            TestTheRequest<IMyApi>(
               api => api.Get5(new decimal[] { 1.2m, 2.4m }),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api?values=1.2_2.4", assertMe.RequestUri.ToString());
               },
               x => x.Request.QueryParameterListStrategy = new DelimitedQueryParamaterListStrategy { Delimiter = "_" });
        }
    }
}
