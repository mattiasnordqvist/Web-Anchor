using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAnchor.RequestFactory.Transformers
{
    public class NoCacheListTransformer : IParameterListTransformer
    {
        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            var parameterList = parameters.ToList();
            parameterList.Add(new Parameter(null, Guid.NewGuid(), ParameterType.Query) { Name = "_" });
            return parameterList;
        }
    }
}