using System;

namespace WebAnchor.Attributes.Header
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