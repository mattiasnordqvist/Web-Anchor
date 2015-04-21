using System;
using System.Collections.Generic;

namespace WebAnchor.RequestFactory.Transformers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface)]
    public abstract class ParameterTransformerAttribute : Attribute, IParameterListTransformer
    {
        public abstract IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters);
    }
}