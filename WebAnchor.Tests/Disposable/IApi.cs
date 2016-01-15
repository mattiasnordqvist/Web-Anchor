using System;

using WebAnchor.RequestFactory;

namespace WebAnchor.Tests.Disposable
{
    [BaseLocation("base")]
    public interface IApi : IDisposable
    {
    }
}