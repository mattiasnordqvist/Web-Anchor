using System;

namespace WebAnchor.RequestFactory
{
    public abstract class ParameterResolverAttribute : Attribute, IParameterResolver
    {
        public abstract void Resolve(Parameter parameter);
    }
}