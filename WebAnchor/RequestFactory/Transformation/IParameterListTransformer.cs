using System;
using System.Collections.Generic;

namespace WebAnchor.RequestFactory.Transformation
{
    public interface IParameterListTransformer
    {
        IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext);

        void ValidateApi(Type type);
    }
}