using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAnchor.RequestFactory.Transformation.Transformers.NoCache
{
    public class NoCacheListTransformer : IParameterListTransformer
    {
        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            var parameterList = parameters.ToList();
            parameterList.Add(new Parameter("_", Guid.NewGuid(), ParameterType.Query));
            return parameterList;
        }

        public void ValidateApi(Type type)
        {
        }
    }
}