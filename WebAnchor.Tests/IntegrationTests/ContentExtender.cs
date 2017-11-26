using System;
using System.Collections.Generic;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers.Default;

namespace WebAnchor.Tests.IntegrationTests
{
    public class ContentExtender : IParameterListTransformer
    {
        public IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext)
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

        public void ValidateApi(Type type)
        {
        }
    }
}