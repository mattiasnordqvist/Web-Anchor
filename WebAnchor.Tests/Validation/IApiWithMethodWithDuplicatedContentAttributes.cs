using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.Content;
using WebAnchor.Attributes.URL;

namespace WebAnchor.Tests.Validation
{
    public interface IApiWithMethodWithDuplicatedContentAttributes
    {
        [Post("")]
        Task<HttpResponseMessage> TaskOfHttpResponseMessage([Content] object c1, [Content] object c2);
    }
}