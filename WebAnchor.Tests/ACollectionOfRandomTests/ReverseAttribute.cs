using System;
using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Attribute.Fixtures
{
    public class ReverseAttribute : ParameterTransformerAttribute, IParameterListTransformer
    {
        public bool CanHandle(Parameter parameter)
        {
            return parameter.Value is string;
        }

        public override void Apply(Parameter parameter, RequestTransformContext requestTransformContext)
        {
            if (CanHandle(parameter))
            {
                parameter.Value = parameter.Value.ToString().Reverse().Aggregate(string.Empty, (x, y) => x + y);
            }
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