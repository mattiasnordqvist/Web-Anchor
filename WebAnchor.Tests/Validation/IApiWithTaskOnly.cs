using System.Threading.Tasks;

namespace WebAnchor.Tests.Validation
{
    public interface IApiWithTaskOnly
    {
        Task TaskOnly();
    }
}