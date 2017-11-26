using System;
using System.Collections.Generic;

namespace WebAnchor.RequestFactory.Transformation
{
    public interface IParameterListTransformer
    {
        IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, RequestTransformContext parameterTransformContext);

        void ValidateApi(Type type);
    }
}