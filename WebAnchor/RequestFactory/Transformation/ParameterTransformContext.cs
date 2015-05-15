using System.Reflection;

namespace WebAnchor.RequestFactory.Transformation
{
    public class ParameterTransformContext
    {
        public ParameterTransformContext(MethodInfo methodInfo)
        {
            this.MethodInfo = methodInfo;
        }

        public MethodInfo MethodInfo { get; private set; }
    }
}