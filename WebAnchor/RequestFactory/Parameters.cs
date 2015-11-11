using System.Collections.Generic;

namespace WebAnchor.RequestFactory
{
    public class Parameters
    {
        public Parameters(IEnumerable<Parameter> routeParameters, IEnumerable<Parameter> queryParameters, IEnumerable<Parameter> headerParameters, Parameter content)
        {
            RouteParameters = routeParameters;
            QueryParameters = queryParameters;
            HeaderParameters = headerParameters;
            Content = content;
        }

        public IEnumerable<Parameter> RouteParameters { get; private set; } 
        public IEnumerable<Parameter> QueryParameters { get; private set; }
        public IEnumerable<Parameter> HeaderParameters { get; private set; }
        public Parameter Content { get; private set; }
    }
}