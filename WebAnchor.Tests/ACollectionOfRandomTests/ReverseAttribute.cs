using System;
using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Attribute.Fixtures
{
    public class ReverseAttribute : ParameterTransformerAttribute, IParameterListTransformer
    {
        public override void Apply(Parameter parameter, RequestTransformContext requestTransformContext)
        {
            parameter.Values = parameter.Values.Select(s => ((string)s).Reverse().Aggregate(string.Empty, (x, y) => x + y)).ToList();
        }

        public IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext)
        {
            foreach (var parameter in parameters)
            {
                Apply(parameter, requestTransformContext);
            }

            return parameters;
        }

        public void ValidateApi(Type type)
        {
        }
    }
}