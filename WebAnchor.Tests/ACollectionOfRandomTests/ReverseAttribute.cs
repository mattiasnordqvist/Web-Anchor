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
            return parameter.SourceValue is string;
        }

        public override void Apply(Parameter parameter)
        {
            if (CanHandle(parameter))
            {
                parameter.Value = parameter.Value.ToString().Reverse().Aggregate(string.Empty, (x, y) => x + y);
            }
        }

        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            foreach (var parameter in parameters)
            {
                Apply(parameter);
            }

            return parameters;
        }

        public void ValidateApi(Type type)
        {
        }
    }
}