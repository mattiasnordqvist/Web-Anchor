using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;
using WebAnchor.RequestFactory.Transformation.Transformers;

namespace WebAnchor.Tests.Validation
{
    public interface IApiWithMethodWithDuplicatedContentAttributes
    {
        [Post("")]
        Task<HttpResponseMessage> TaskOfHttpResponseMessage([Content] object c1, [Content] object c2);
    }
}