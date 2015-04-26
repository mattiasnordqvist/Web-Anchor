using System;

namespace WebAnchor.RequestFactory.Resolvers
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class AliasAttribute : ParameterResolverAttribute
    {
        private readonly string _alias;

        public AliasAttribute(string alias)
        {
            _alias = alias;
        }

        public override void Resolve(Parameter parameter)
        {
            parameter.Name = _alias;
        }
    }
}
