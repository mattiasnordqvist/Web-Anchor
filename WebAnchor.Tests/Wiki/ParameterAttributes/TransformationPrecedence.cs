using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.Parameters;
using WebAnchor.Attributes.URL;
using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.Tests.TestUtils;
using Xunit;

namespace WebAnchor.Tests.Wiki
{
    public class TransformationPrecedenceTests : WebAnchorTest
    {
        #region precedence
        [BaseLocation("api/customer")]
        [Prefix("p_")]
        public interface ICustomerApi
        {
            [Get("")]
            Task<HttpResponseMessage> GetCustomers([Alias("f")]string filter = null, int? age = null);
        }

        [Fact]
        public void TestPrecedence()
        {
            TestTheRequest<ICustomerApi>(
               api => api.GetCustomers("new", 28),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("api/customer?f=new&p_age=28", assertMe.RequestUri.ToString());
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

            public override void Apply(Parameter parameter)
            {
                parameter.Name = _prefix + parameter.SourceParameterInfo.Name;
            }
        }
        #endregion
    }
}
