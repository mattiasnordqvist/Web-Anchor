using System.Net.Http;
using System.Threading.Tasks;

using WebAnchor.RequestFactory.HttpAttributes;

namespace WebAnchor.Tests.Validation
{
    public interface IApiWithTaskOfHttpResponseMessage
    {
        [Get("")]
        Task<HttpResponseMessage> TaskOfHttpResponseMessage();
    }
}