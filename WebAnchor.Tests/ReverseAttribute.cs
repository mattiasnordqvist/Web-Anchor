using System;
using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute;

namespace WebAnchor.Tests
{
    public class ReverseAttribute : ParameterTransformerAttribute, IParameterListTransformer
    {
        public bool CanResolve(Parameter parameter)
        {
            return parameter.ParameterValue is string;
        }

        public override void Apply(Parameter parameter)
        {
            if (CanResolve(parameter))
            {
                parameter.Value = parameter.Value.ToString().Reverse().Aggregate(string.Empty, (x, y) => x + y);
            }
        }

        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            foreach (var parameter in parameters)
            {
                this.Apply(parameter);
            }

            return parameters;
        }

        public void ValidateApi(Type type)
        {
        }
    }
}