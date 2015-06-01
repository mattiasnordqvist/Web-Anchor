using System.Threading.Tasks;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.HttpAttributes;
using WebAnchor.RequestFactory.Transformation.Transformers;

namespace WebAnchor.Tests.ProofOfConcepts.HeaderExtraction.Fixtures
{
    [BaseLocation("api/customer")]
    public interface ICustomerApi
    {
        [Post("/extension")]
        Task<CustomerWithLocation> CreateCustomer([Content]Customer customer);
    }
}
