using System;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Headers
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
    public class AuthorizationAttribute : HeaderAttribute
    {
        public AuthorizationAttribute(string value)
            : base("Authorization", value)
        {
        }
    }
}