using System.Net.Http;
using System.Threading.Tasks;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.HttpAttributes;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Attribute.Fixtures
{
    [BaseLocation("api/customer")]
    public interface ICustomerApiWithAttributedMethods
    {
        [Get("/{id}")]
        Task<HttpResponseMessage> GetCustomer_PrefixedQueryParamValue([Multiply]int id);

        [Get("")]
        Task<HttpResponseMessage> GetCustomers_PrefixedQueryParamName([Prefix("p_")]string filter = null);

        [Get("")]
        Task<HttpResponseMessage> GetCustomers_ReversedQueryParamValue([Reverse]string filter = null);
    }
}
