using System.Threading.Tasks;

using WebAnchor.RequestFactory.HttpAttributes;

namespace WebAnchor.Tests.Validation
{
    public interface ApiWithTaskOfT
    {
        [Get("")]
        Task<object> TaskOfT();
    }
}