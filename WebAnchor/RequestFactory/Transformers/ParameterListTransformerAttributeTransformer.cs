using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory.Transformers;

namespace WebAnchor.RequestFactory
{
    public class ParameterListTransformerAttributeTransformer : IParameterListTransformer
    {
        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            return parameterTransformContext.MethodInfo.GetAttributesChain<ParameterListTransformerAttribute>()
                                            .Aggregate(parameters, (current, transformer) =>
                                                transformer.TransformParameters(current, parameterTransformContext));
        }
    }
}