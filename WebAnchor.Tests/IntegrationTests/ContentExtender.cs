using System.Collections.Generic;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers.Default;

namespace WebAnchor.Tests.IntegrationTests
{
    public class ContentExtender : ParameterListTransformerBase
    {
        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            foreach (var parameter in parameters)
            {
                if (parameter.ParameterType == ParameterType.Content)
                {
                    parameter.Value = parameter.Value.ToDictionary();
                    ((Dictionary<string, object>)parameter.Value)["Name"] = "Mighty Gazelle";
                }
            }

            return parameters;
        }
    }
}