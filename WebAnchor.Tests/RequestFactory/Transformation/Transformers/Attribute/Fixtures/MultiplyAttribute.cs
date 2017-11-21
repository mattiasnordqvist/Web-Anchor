using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Attribute.Fixtures
{
    public class MultiplyAttribute : ParameterTransformerAttribute
    {
        public bool CanResolve(Parameter parameter)
        {
            int dummy;
            return parameter.SourceValue != null && int.TryParse(parameter.SourceValue.ToString(), out dummy);
        }

        public override void Apply(Parameter parameter)
        {
            if (CanResolve(parameter))
            {
                parameter.Value = (((int)parameter.SourceValue) * 10).ToString();
            }
        }
    }
}