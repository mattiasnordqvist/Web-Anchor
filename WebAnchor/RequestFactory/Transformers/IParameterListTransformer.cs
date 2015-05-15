using System.Collections.Generic;

namespace WebAnchor.RequestFactory
{
    public interface IParameterListTransformer
    {
        IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext);
    }
}