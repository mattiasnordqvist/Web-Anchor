using WebAnchor.RequestFactory;

namespace WebAnchor.Tests
{
    public class PrefixAttribute : ParameterResolverAttribute
    {
        private readonly string _prefix;

        public PrefixAttribute(string prefix)
        {
            _prefix = prefix;
        }

        public override void Resolve(Parameter parameter)
        {
            parameter.Name = _prefix + parameter.ParameterInfo.Name;
        }
    }
}