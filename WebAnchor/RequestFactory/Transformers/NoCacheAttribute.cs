using System.Collections.Generic;

namespace WebAnchor.RequestFactory.Transformers
{
    public class NoCacheAttribute : ParameterTransformerAttribute
    {
        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters)
        {
            return new NoCacheListTransformer().TransformParameters(parameters);
        }
    }
}