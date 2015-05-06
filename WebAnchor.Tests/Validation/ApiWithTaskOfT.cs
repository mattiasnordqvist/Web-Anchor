using System.Threading.Tasks;

namespace WebAnchor.Tests.Validation
{
    public interface ApiWithTaskOfT
    {
        Task<object> TaskOfT();
    }
}