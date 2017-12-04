using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAnchor.RequestFactory.Transformation.Transformers.NoCache
{
    [Obsolete("The NoCacheAttriute will be removed in a future version. You can implement it yourself, it's easy!")]
    public class NoCacheListTransformer : ParameterListTransformerBase
    {
        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            var parameterList = parameters.ToList();
            parameterList.Add(new Parameter(null, Guid.NewGuid(), ParameterType.Query) { Name = "_" });
            return parameterList;
        }
    }
}