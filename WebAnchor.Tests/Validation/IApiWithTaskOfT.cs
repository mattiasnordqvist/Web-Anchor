using System.Threading.Tasks;
using WebAnchor.Attributes.URL;

namespace WebAnchor.Tests.Validation
{
    public interface IApiWithTaskOfT
    {
        [Get("")]
        Task<object> TaskOfT();
    }
}