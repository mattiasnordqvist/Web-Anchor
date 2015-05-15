using System;
using System.Collections.Generic;

namespace WebAnchor.RequestFactory.Transformation
{
    public abstract class ParameterListTransformerBase : IParameterListTransformer
    {
        public abstract IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext);

        public void ValidateApi(Type type)
        {
        }
    }
}