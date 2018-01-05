using System.Collections.Generic;

using Newtonsoft.Json;

using WebAnchor.ResponseParser.ResponseHandlers;

namespace WebAnchor.ResponseParser
{
    public class DefaultResponseHandlers : List<IResponseHandler>
    {
        public DefaultResponseHandlers()
        {
            Add(new AsyncTaskHandler());
            Add(new AsyncHttpResponseMessageResponseHandler());
            Add(new AsyncDeserializingResponseHandler(new JsonContentDeserializer(new JsonSerializer())));
        }
    }
}