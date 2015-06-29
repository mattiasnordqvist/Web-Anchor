using System.Threading.Tasks;

using WebAnchor.RequestFactory.HttpAttributes;

namespace WebAnchor.Tests.ACollectionOfRandomTests.Fixtures
{
    public interface ITypedApi<T>
    {
        [Get("/return")]
        Task<T> GetSameObject(int id, string name);
    }
}