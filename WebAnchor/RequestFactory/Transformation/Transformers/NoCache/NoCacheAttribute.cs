using System.Collections.Generic;

using WebAnchor.RequestFactory.Transformation.Transformers.Attribute.List;

namespace WebAnchor.RequestFactory.Transformation.Transformers.NoCache
{
    public class NoCacheAttribute : ParameterListTransformerAttribute
    {
        public override IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext parameterTransformContext)
        {
            return new NoCacheListTransformer().TransformParameters(parameters, parameterTransformContext);
        }
    }
}