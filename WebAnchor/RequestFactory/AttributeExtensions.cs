using System;
using System.Linq;
using System.Reflection;

namespace WebAnchor.RequestFactory
{
    public static class AttributeExtensions
    {
        public static T GetFirstAttributeInChain<T>(this ParameterInfo @this) where T : Attribute
        {
            return @this.GetCustomAttributes<T>().Any() ? @this.GetCustomAttributes<T>().First() : @this.Member.GetFirstAttributeInChain<T>();
        }

        public static T GetFirstAttributeInChain<T>(this MemberInfo @this) where T : Attribute
        {
            return @this.GetCustomAttributes<T>().Any() ? @this.GetCustomAttributes<T>().First() : @this.DeclaringType.GetFirstAttributeInChain<T>();
        }

        public static T GetFirstAttributeInChain<T>(this Type @this) where T : Attribute
        {
            var typeInfo = @this.GetTypeInfo();
            return typeInfo.GetCustomAttributes<T>().Any() ? typeInfo.GetCustomAttributes<T>().First() : null;
        }
    }
}