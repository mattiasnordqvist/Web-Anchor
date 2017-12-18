using System;
using System.Linq;
using System.Reflection;

namespace WebAnchor.RequestFactory
{
    public static class AttributeExtensions
    {
        /// <summary>
        /// Looks for an attribute on a parameter. If not found, continues to search at owning method.
        /// </summary>
        public static T GetFirstAttributeInChain<T>(this ParameterInfo @this) where T : Attribute
        {
            return @this.GetCustomAttributes<T>().Any() ? @this.GetCustomAttributes<T>().First() : @this.Member.GetFirstAttributeInChain<T>();
        }

        /// <summary>
        /// Looks for an attribute on a method. If not found, continues to search at owning type.
        /// </summary>
        public static T GetFirstAttributeInChain<T>(this MemberInfo @this) where T : Attribute
        {
            return @this.GetCustomAttributes<T>().Any() ? @this.GetCustomAttributes<T>().First() : @this.DeclaringType.GetFirstAttributeInChain<T>();
        }

        /// <summary>
        /// Looks for an attribute on a type. If not found, returns null.
        /// </summary>
        public static T GetFirstAttributeInChain<T>(this Type @this) where T : Attribute
        {
            var typeInfo = @this.GetTypeInfo();
            return typeInfo.GetCustomAttributes<T>().Any() ? typeInfo.GetCustomAttributes<T>().First() : null;
        }
    }
}