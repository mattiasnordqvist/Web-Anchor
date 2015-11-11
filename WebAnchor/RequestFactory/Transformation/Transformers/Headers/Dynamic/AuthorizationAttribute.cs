using System;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Headers.Dynamic
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class AuthorizationAttribute : HeaderAttribute
    {
        public AuthorizationAttribute()
            : base("Authorization")
        {
        }
    }
}