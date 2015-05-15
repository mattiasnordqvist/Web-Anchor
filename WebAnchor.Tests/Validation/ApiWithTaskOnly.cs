using System.Threading.Tasks;

namespace WebAnchor.Tests.Validation
{
    public interface ApiWithTaskOnly
    {
        Task TaskOnly();
    }
}