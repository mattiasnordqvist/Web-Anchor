using System.Reflection;

namespace WebAnchor.RequestFactory.Transformation
{
    public class RequestTransformContext
    {
        public RequestTransformContext(ApiInvocation apiInvocation)
        {
            MethodInfo = apiInvocation.Method;
            ApiInvocation = apiInvocation;
        }

        public MethodInfo MethodInfo { get; private set; }
        public ApiInvocation ApiInvocation { get; private set; }
        public string UrlTemplate { get; internal set; }
    }
}