using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Resolvers;

namespace WebAnchor.Tests
{
    public class PrefixAttribute : ParameterTransformerAttribute
    {
        private readonly string _prefix;

        public PrefixAttribute(string prefix)
        {
            _prefix = prefix;
        }

        public override void Apply(Parameter parameter)
        {
            parameter.Name = _prefix + parameter.ParameterInfo.Name;
        }
    }
}