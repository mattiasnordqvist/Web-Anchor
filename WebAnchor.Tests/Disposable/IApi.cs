using System;
using System.Threading.Tasks;

using WebAnchor.Attributes.URL;

namespace WebAnchor.Tests.Disposable
{
    [BaseLocation("base")]
    public interface IApi : IDisposable
    {
        [Get]
        public Task Dummy();
    }
}