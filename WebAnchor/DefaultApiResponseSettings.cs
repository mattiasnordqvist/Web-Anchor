using System.Collections.Generic;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers.Default;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    public class DefaultApiResponseSettings : IApiResponeSettings
    {
        public DefaultApiResponseSettings()
        {
            ResponseHandlers = new DefaultResponseHandlers();
        }

        public virtual List<IResponseHandler> ResponseHandlers { get; set; }
        public IDictionary<string, object> CustomSettings { get; set; } = new Dictionary<string, object>();
    }
}