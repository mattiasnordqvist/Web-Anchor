using System.Collections.Generic;
using Newtonsoft.Json;
using WebAnchor.ResponseParser;
using WebAnchor.ResponseParser.ResponseHandlers;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Default
{
    public class DefaultResponseHandlers : List<IResponseHandler>
    {
        public DefaultResponseHandlers()
        {
            Add(new AsyncHttpResponseMessageResponseHandler());
            Add(new AsyncDeserializingResponseHandler(new JsonContentDeserializer(new JsonSerializer())));
        }
    }
}