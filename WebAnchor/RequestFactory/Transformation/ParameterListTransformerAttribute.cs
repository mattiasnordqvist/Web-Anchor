using System;
using System.Collections.Generic;
using System.Reflection;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Attribute.List
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface)]
    public abstract class ParameterListTransformerAttribute : System.Attribute
    {
        public abstract IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext parameterTransformContext);

        public virtual void ValidateApi(Type type, MethodInfo method) { }
    }
}