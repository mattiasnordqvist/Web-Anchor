using System.Collections.Generic;
using Newtonsoft.Json;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Resolvers;
using WebAnchor.RequestFactory.Transformers;
using WebAnchor.ResponseParser;

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
            return new HttpResponseParser(CreateContentDeserializer());
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
                new ParameterOfListTransformer(),
                new DefaultParameterResolver(),
                new FormattableParameterResolver(),
                new ParameterListTransformerAttributeTransformer(),
                new ParameterTransformerAttributeTransformer(),
            };
        }
    }
}