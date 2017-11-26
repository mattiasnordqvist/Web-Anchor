using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;
using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.Tests.TestUtils;
using Xunit;

namespace WebAnchor.Tests.Wiki
{
    public class ExamplePrefixTests : WebAnchorTest
    {
        #region usages
        [BaseLocation("api/customer")]
        public interface ICustomerApi
        {
            [Get("")]
            Task<HttpResponseMessage> GetCustomers([Prefix("p_")]string filter = null);
        }

        [BaseLocation("api/customer")]
        public interface ICustomerApi2
        {
            [Get("")]
            [Prefix("p_")]
            Task<HttpResponseMessage> GetCustomers(string filter = null, int? age = null);

            [Get("")]
            Task<HttpResponseMessage> GetCustomersNoPrefixes(string filter = null, int? age = null);
        }

        [BaseLocation("api/customer")]
        [Prefix("p_")]
        public interface ICustomerApi3
        {
            [Get("")]
            Task<HttpResponseMessage> GetCustomers(string filter = null, int? age = null);
        }
        #endregion

        #region usage on parameter
        
        [Fact]
        public void TestExplicitPrefixAttribute()
        {
            TestTheRequest<ICustomerApi>(
               api => api.GetCustomers("new"),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api/customer?p_filter=new", assertMe.RequestUri.ToString());
               });
        }

        #endregion

        #region usage on method

        [Fact]
        public void TestImplicitPrefixAttribute()
        {
            TestTheRequest<ICustomerApi2>(
               api => api.GetCustomers("new", 28),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api/customer?p_filter=new&p_age=28", assertMe.RequestUri.ToString());
               });
        }

        [Fact]
        public void TestImplicitPrefixAttributeNoPrefixes()
        {
            TestTheRequest<ICustomerApi2>(
               api => api.GetCustomersNoPrefixes("new", 28),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api/customer?filter=new&age=28", assertMe.RequestUri.ToString());
               });
        }
        #endregion

        #region usage on interface
        [Fact]
        public void TestImplicitPrefixAttributeOnInterface()
        {
            TestTheRequest<ICustomerApi3>(
               api => api.GetCustomers("new", 28),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api/customer?p_filter=new&p_age=28", assertMe.RequestUri.ToString());
               });
        }
        #endregion

        #region the attribute
        public class PrefixAttribute : ParameterTransformerAttribute
        {
            private readonly string _prefix;

            public PrefixAttribute(string prefix)
            {
                _prefix = prefix;
            }

            public override void Apply(Parameter parameter, RequestTransformContext requestTransformContext)
            {
                parameter.Name = _prefix + parameter.Name;
            }
        }
        #endregion
    }
}
