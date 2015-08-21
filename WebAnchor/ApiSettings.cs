using System.Collections.Generic;
using Newtonsoft.Json;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute.List;
using WebAnchor.RequestFactory.Transformation.Transformers.Default;
using WebAnchor.RequestFactory.Transformation.Transformers.Formattable;
using WebAnchor.RequestFactory.Transformation.Transformers.List;
using WebAnchor.ResponseParser;
using WebAnchor.ResponseParser.ResponseHandlers;

namespace WebAnchor
{
    public class ApiSettings : ISettings
    {
        public IHttpRequestFactory GetRequestFactory()
        {
            return new HttpRequestFactory(CreateContentSerializer(), CreateParameterListTransformers());
        }

        public IHttpResponseParser GetResponseParser()
        {
            return new HttpResponseParser(CreateResponseHandlers());
        }

        public virtual IContentSerializer CreateContentSerializer()
        {
            return new ContentSerializer(new JsonSerializer());
        }

        public virtual IContentDeserializer CreateContentDeserializer()
        {
            return new JsonContentDeserializer(new JsonSerializer());
        }

        public virtual IList<IParameterListTransformer> CreateParameterListTransformers()
        {
            return new List<IParameterListTransformer>
            {
                new ParameterCreatorTransformer(),
                new ParameterOfListTransformer(),
                new DefaultParameterTransformer(),
                new FormattableParameterResolver(),
                new ParameterListTransformerAttributeTransformer(),
                new ParameterTransformerAttributeTransformer(),
            };
        }

        public virtual IList<IResponseHandler> CreateResponseHandlers()
        {
            return new List<IResponseHandler>
                       {
                           new AsyncHttpResponseMessageResponseHandler(),
                           new AsyncDeserializingResponseHandler(CreateContentDeserializer()),
                       };
        }
    }
}