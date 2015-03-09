using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory;

namespace WebAnchor.Tests
{
    public class ReverseParameterListTransformers : IParameterListTransformer
    {
        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters)
        {
            return parameters.Reverse();
        }
    }
}