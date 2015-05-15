using System;

namespace WebAnchor.RequestFactory.Transformers.Attribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Parameter)]
    public abstract class ParameterTransformerAttribute : System.Attribute
    {
        public ParameterTransformContext Context { get; set; }
        public abstract void Apply(Parameter parameter);
    }
}