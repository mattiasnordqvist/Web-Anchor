using System.Net.Http;
using System.Threading.Tasks;

using WebAnchor.RequestFactory.HttpAttributes;
using WebAnchor.RequestFactory.Transformation.Transformers;
using WebAnchor.RequestFactory.Transformation.Transformers.Default;

namespace WebAnchor.Tests.Validation
{
    public interface IApiWithMethodWithDuplicatedContentAttributes
    {
        [Post("")]
        Task<HttpResponseMessage> TaskOfHttpResponseMessage([Content] object c1, [Content] object c2);
    }
}