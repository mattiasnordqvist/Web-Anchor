using System.Threading.Tasks;
using WebAnchor.Attributes.Content;
using WebAnchor.Attributes.URL;
using WebAnchor.Tests.ACollectionOfRandomTests.Fixtures;

namespace WebAnchor.Tests.ProofOfConcepts.ParsingTheLocationHeader.Fixtures
{
    [BaseLocation("api/customer")]
    public interface ICustomerApi
    {
        [Post("/extension")]
        Task<CustomerWithLocation> CreateCustomer([Content]Customer customer);
    }
}
