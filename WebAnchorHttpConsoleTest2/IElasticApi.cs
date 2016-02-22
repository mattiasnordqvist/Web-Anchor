using System;
using System.Threading.Tasks;
using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.HttpAttributes;

namespace WebAnchorHttpConsoleTest2
{
    [BaseLocation("")]
    public interface IElasticApi : IDisposable
    {
        [Get("")]
        Task<ElasticSearchStatusReport> GetElasticSearchStatusReport();
    }
}