using System.Collections.Generic;
using WebAnchor.ResponseParser;

namespace WebAnchor.ResponseParser
{
    public class DefaultApiResponseSettings : IApiResponeSettings
    {
        public DefaultApiResponseSettings()
        {
            ResponseHandlers = new DefaultResponseHandlers();
        }

        public virtual List<IResponseHandler> ResponseHandlers { get; set; }
    }
}