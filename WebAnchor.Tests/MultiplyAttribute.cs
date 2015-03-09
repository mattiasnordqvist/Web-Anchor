using WebAnchor.RequestFactory;

namespace WebAnchor.Tests
{
    public class MultiplyAttribute : ParameterResolverAttribute
    {
        public bool CanResolve(Parameter parameter)
        {
            int dummy;
            return parameter.ParameterValue != null && int.TryParse(parameter.ParameterValue.ToString(), out dummy);
        }

        public override void Resolve(Parameter parameter)
        {
            if (CanResolve(parameter))
            {
                parameter.Value = (((int)parameter.ParameterValue) * 10).ToString();
            }
        }
    }
}