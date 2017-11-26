using System.Reflection;

namespace WebAnchor.RequestFactory.Transformation
{
    public class ParameterTransformContext
    {
        public ParameterTransformContext(ApiInvocation apiInvocation)
        {
            MethodInfo = apiInvocation.Method;
            ApiInvocation = apiInvocation;
        }

        public MethodInfo MethodInfo { get; private set; }
        public ApiInvocation ApiInvocation { get; private set; }
        public string UrlTemplate { get; internal set; }
    }
}