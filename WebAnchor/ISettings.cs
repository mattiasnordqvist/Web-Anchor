using System.Collections.Generic;
using WebAnchor.RequestFactory;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    public interface ISettings
    {
        IHttpRequestFactory GetRequestFactory();
        IHttpResponseParser GetResponseParser();
        IContentSerializer CreateContentSerializer();
        IContentDeserializer CreateContentDeserializer();
        IList<IParameterListTransformer> CreateParameterListTransformers();
    }
}