using System.Collections.Generic;

namespace WebAnchor.RequestFactory
{
    public class Parameters
    {
        public Parameters(IEnumerable<Parameter> routeParameters, IEnumerable<Parameter> queryParameters, Parameter payLoad)
        {
            QueryParameters = queryParameters;
            PayLoad = payLoad;
            RouteParameters = routeParameters;
        }

        public IEnumerable<Parameter> RouteParameters { get; private set; } 
        public IEnumerable<Parameter> QueryParameters { get; private set; }
        public Parameter PayLoad { get; private set; }
    }
}