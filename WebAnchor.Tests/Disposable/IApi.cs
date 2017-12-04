using System;
using WebAnchor.Attributes.URL;

namespace WebAnchor.Tests.Disposable
{
    [BaseLocation("base")]
    public interface IApi : IDisposable
    {
    }
}