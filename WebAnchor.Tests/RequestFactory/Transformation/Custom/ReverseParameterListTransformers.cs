using System;
using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Tests.RequestFactory.Transformation.Custom
{
    public class ReverseParameterListTransformers : IParameterListTransformer
    {
        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            return parameters.Reverse();
        }

        public void ValidateApi(Type type)
        {
        }
    }
}