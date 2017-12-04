using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;

namespace WebAnchor.Tests.Validation
{
    public interface IApiWithTaskOfHttpResponseMessage
    {
        [Get("")]
        Task<HttpResponseMessage> TaskOfHttpResponseMessage();
    }
}