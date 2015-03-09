using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebAnchor.RequestFactory
{
    public static class AttributeExtensions
    {
        public static IEnumerable<T> GetResolverAttributesChain<T>(this ParameterInfo @this) where T : Attribute, IParameterResolver
        {
            return @this.GetCustomAttributes<T>().Concat(GetResolverAttributesChain<T>(@this.Member));
        }

        private static IEnumerable<T> GetResolverAttributesChain<T>(this MemberInfo @this) where T : Attribute, IParameterResolver
        {
            return @this.GetCustomAttributes<T>().Concat(GetResolverAttributesChain<T>(@this.DeclaringType));
        }

        private static IEnumerable<T> GetResolverAttributesChain<T>(this Type @this) where T : Attribute, IParameterResolver
        {
            return @this.GetCustomAttributes<T>();
        }
    }
}