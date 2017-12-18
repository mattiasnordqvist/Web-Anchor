using System.Collections.Generic;

namespace WebAnchor.ResponseParser
{
    public interface IApiResponeSettings
    {
        List<IResponseHandler> ResponseHandlers { get; set; }
    }
}