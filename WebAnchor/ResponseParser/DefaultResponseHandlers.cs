using System.Collections.Generic;

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
            Add(new AsyncDeserializingResponseHandler(new JsonContentDeserializer()));
        }
    }
}