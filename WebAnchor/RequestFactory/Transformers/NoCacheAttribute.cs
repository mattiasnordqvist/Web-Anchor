using System.Collections.Generic;

namespace WebAnchor.RequestFactory.Transformers
{
    public class NoCacheAttribute : ParameterListTransformerAttribute
    {
        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            return new NoCacheListTransformer().TransformParameters(parameters, parameterTransformContext);
        }
    }
}