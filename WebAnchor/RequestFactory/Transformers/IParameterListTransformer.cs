using System.Collections.Generic;

namespace WebAnchor.RequestFactory.Transformers
{
    public interface IParameterListTransformer
    {
        IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext);
    }
}