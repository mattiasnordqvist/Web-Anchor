using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;
using WebAnchor.RequestFactory.Transformation.Transformers;

namespace WebAnchor.Tests.PayloadDependentUrlSegments
{
    [BaseLocation("/api")]
    [TypeNameAsUrlParameterList]
    public interface IDynamicTypedApi<in T>
    {
        [Post("/{type}")]
        Task<HttpResponseMessage> PostThis([Content]T t);
    }

    [BaseLocation("/api")]
    [TypeNameAsUrlParameter2]
    public interface IDynamicTypedApi2<in T>
    {
        [Post("/{type}")]
        Task<HttpResponseMessage> PostThis([Content]T t);
    }

    [BaseLocation("/api")]
    [UseTypeInUrl]
    public interface IAnyResourceApi<T>
    {
        [Get("/{type}/{id}")]
        Task<T> Get(int id);
    }
}