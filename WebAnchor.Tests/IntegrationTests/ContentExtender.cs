using System.Collections.Generic;

using WebAnchor.RequestFactory;

namespace WebAnchor.Tests.IntegrationTests
{
    public class ContentExtender : IParameterResolver
    {
        public void Resolve(Parameter parameter)
        {
            if (parameter.ParameterType == ParameterType.Content)
            {
                ((Dictionary<string, object>)parameter.Value)["Name"] = "Mighty Gazelle";
            }
        }
    }
}