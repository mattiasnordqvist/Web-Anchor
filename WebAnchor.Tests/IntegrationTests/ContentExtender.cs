using System.Collections.Generic;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Resolvers;
using WebAnchor.RequestFactory.Transformers;

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