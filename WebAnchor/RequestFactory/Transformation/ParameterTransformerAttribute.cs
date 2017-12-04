using System;
using System.Reflection;

namespace WebAnchor.RequestFactory.Transformation
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Parameter)]
    public abstract class ParameterTransformerAttribute : Attribute
    {
        public abstract void Apply(Parameter parameter, RequestTransformContext requestTransformContext);
        public virtual void ValidateApi(Type type, MethodInfo method, ParameterInfo parameter) { }
    }
}