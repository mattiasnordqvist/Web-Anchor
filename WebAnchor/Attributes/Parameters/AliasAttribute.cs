using System;
using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Attributes.Parameters
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class AliasAttribute : ParameterTransformerAttribute
    {
        public AliasAttribute(string alias)
        {
            Alias = alias;
        }

        public string Alias { get; private set; }

        public override void Apply(Parameter parameter)
        {
            parameter.Name = Alias;
        }
    }
}
