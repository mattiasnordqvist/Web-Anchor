using System;
using System.Net.Http;
using System.Threading.Tasks;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.HttpAttributes;

namespace WebAnchor.Tests.Disposable
{
    [BaseLocation("base")]
    public interface IApi : IDisposable
    {
        [Get("/path1")]
        Task<HttpResponseMessage> Get();
    }
}