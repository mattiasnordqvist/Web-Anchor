using System.Threading.Tasks;

using WebAnchor.RequestFactory.HttpAttributes;

namespace WebAnchor.Tests.Validation
{
    public interface IApiWithTaskOfT
    {
        [Get("")]
        Task<object> TaskOfT();
    }
}