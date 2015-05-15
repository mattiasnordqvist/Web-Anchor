using System.Threading.Tasks;

namespace WebAnchor.Tests.Validation
{
    public interface IApiWithTaskOfT
    {
        Task<object> TaskOfT();
    }
}