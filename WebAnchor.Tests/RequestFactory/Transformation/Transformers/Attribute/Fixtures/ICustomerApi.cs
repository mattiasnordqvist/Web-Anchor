using System.Net.Http;
using System.Threading.Tasks;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.HttpAttributes;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Attribute.Fixtures
{
    [BaseLocation("api/customer")]
    public interface ICustomerApi
    {
            [Get("")]
            Task<HttpResponseMessage> GetCustomers2([Prefix("p_")]string filter = null);
    }
}
