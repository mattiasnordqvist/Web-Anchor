using System.Collections.Generic;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Tests.IntegrationTests
{
    public class ContentExtender : IParameterListTransformer
    {
        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            foreach (var parameter in parameters)
            {
                if (parameter.ParameterType == ParameterType.Content)
                {
                    ((Dictionary<string, object>)parameter.Value)["Name"] = "Mighty Gazelle";
                }
            }

            return parameters;
        }
    }
}