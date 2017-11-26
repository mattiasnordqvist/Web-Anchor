using System;

namespace WebAnchor.RequestFactory.Transformation
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Parameter)]
    public abstract class ParameterTransformerAttribute : Attribute
    {
        public ParameterTransformContext Context { get; set; }
        public abstract void Apply(Parameter parameter);
    }
}