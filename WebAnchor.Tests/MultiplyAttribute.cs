using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute;

namespace WebAnchor.Tests
{
    public class MultiplyAttribute : ParameterTransformerAttribute
    {
        public bool CanResolve(Parameter parameter)
        {
            int dummy;
            return parameter.ParameterValue != null && int.TryParse(parameter.ParameterValue.ToString(), out dummy);
        }

        public override void Apply(Parameter parameter)
        {
            if (CanResolve(parameter))
            {
                parameter.Value = (((int)parameter.ParameterValue) * 10).ToString();
            }
        }
    }
}