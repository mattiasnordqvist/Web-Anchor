using System;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Headers
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
    public class AddAuthorizationAttribute : AddHeaderAttribute
    {
        public AddAuthorizationAttribute(string value)
            : base("Authorization", value)
        {
        }
    }
}