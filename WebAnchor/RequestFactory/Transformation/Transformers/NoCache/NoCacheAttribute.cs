using System;
using System.Collections.Generic;

using WebAnchor.RequestFactory.Transformation.Transformers.Attribute.List;

namespace WebAnchor.RequestFactory.Transformation.Transformers.NoCache
{
    [Obsolete("The NoCacheAttriute will be removed in a future version. You can implement it yourself, it's easy!")]
    public class NoCacheAttribute : ParameterListTransformerAttribute
    {
        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            return new NoCacheListTransformer().TransformParameters(parameters, parameterTransformContext);
        }
    }
}