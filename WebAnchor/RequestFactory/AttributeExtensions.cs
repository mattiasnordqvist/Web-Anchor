using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebAnchor.RequestFactory
{
    public static class AttributeExtensions
    {
        public static IEnumerable<T> GetAttributesChain<T>(this ParameterInfo @this) where T : Attribute
        {
            return @this.GetCustomAttributes<T>().Concat(GetAttributesChain<T>(@this.Member));
        }

        public static IEnumerable<T> GetAttributesChain<T>(this MemberInfo @this) where T : Attribute
        {
            return @this.GetCustomAttributes<T>().Concat(GetAttributesChain<T>(@this.DeclaringType));
        }

        private static IEnumerable<T> GetAttributesChain<T>(this Type @this) where T : Attribute
        {
            return @this.GetCustomAttributes<T>();
        }
    }
}