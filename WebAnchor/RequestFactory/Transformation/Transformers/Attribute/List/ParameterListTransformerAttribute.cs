using System;
using System.Collections.Generic;

namespace WebAnchor.RequestFactory.Transformers.Attribute.List
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface)]
    public abstract class ParameterListTransformerAttribute : System.Attribute
    {
        public abstract IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext);
    }
}