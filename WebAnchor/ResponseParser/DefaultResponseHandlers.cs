using System.Collections.Generic;
using System.Text.Json;

using WebAnchor.ResponseParser.ResponseHandlers;

namespace WebAnchor.ResponseParser
{
    public class DefaultResponseHandlers : List<IResponseHandler>
    {
        public DefaultResponseHandlers()
        {
            Add(new AsyncTaskHandler());
            Add(new AsyncStreamHandler());
            Add(new AsyncHttpResponseMessageResponseHandler());
            Add(new AsyncDeserializingResponseHandler(new JsonContentDeserializer(new JsonSerializerOptions())));
        }
    }
}