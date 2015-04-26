using System.Collections.Generic;

namespace WebAnchor.RequestFactory
{
    public class Parameters
    {
        public Parameters(IEnumerable<Parameter> routeParameters, IEnumerable<Parameter> queryParameters, Parameter content)
        {
            QueryParameters = queryParameters;
            this.Content = content;
            RouteParameters = routeParameters;
        }

        public IEnumerable<Parameter> RouteParameters { get; private set; } 
        public IEnumerable<Parameter> QueryParameters { get; private set; }
        public Parameter Content { get; private set; }
    }
}