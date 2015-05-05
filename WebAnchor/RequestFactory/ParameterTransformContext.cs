using System.Reflection;

namespace WebAnchor.RequestFactory
{
    public class ParameterTransformContext
    {
        public ParameterTransformContext(ApiInvocation apiInvocation)
        {
            this.MethodInfo = apiInvocation.Method;
            this.ApiInvocation = apiInvocation;
        }

        public MethodInfo MethodInfo { get; private set; }
        public ApiInvocation ApiInvocation { get; private set; }
    }
}