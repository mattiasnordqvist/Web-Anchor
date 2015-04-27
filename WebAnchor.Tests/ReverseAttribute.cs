using System.Linq;

using WebAnchor.RequestFactory;

namespace WebAnchor.Tests
{
    public class ReverseAttribute : ParameterResolverAttribute
    {
        public bool CanResolve(Parameter parameter)
        {
            return parameter.ParameterValue is string;
        }

        public override void Resolve(Parameter parameter)
        {
            if (CanResolve(parameter))
            {
                parameter.Value = parameter.Value.ToString().Reverse().Aggregate(string.Empty, (x, y) => x + y);
            }
        }
    }
}