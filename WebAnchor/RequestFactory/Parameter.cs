using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebAnchor.RequestFactory
{
    public class Parameter
    {
        private Parameter()
        {
        }

        /// <summary>
        /// The source value from api method parameters.
        /// </summary>
        public object SourceValue { get; private set; }

        /// <summary>
        /// The parameterinfo of the SourceValue.
        /// </summary>
        public ParameterInfo SourceParameterInfo { get; private set; }

        /// <summary>
        /// If this parameter was created from another parameter, that other parameter would be the parent parameter
        /// </summary>
        public Parameter ParentParameter { get; set; }

        /// <summary>
        /// Defines where this parameter will be used.
        /// </summary>
        public ParameterType ParameterType { get; set; }

        /// <summary>
        /// Name of parameter could be used to find url segment to be substituted, or as name of a query param, or as the name of a header.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value that will probably be ToString()ed and used in the actual http request, or maybe serialized as json and used as content.
        /// </summary>
        public IEnumerable<object> Values { get; set; }

        /// <summary>
        /// value should be a single value, not a list or any other enumerable
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Parameter CreateContentParameter(string name, object value)
        {
            return new Parameter()
            {
                Values = new object[] { value },
                ParameterType = ParameterType.Content,
                Name = name,
            };
        }

        /// <summary>
        /// value should be a single value, not a list or any other enumerable
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Parameter CreateRouteParameter(string name, object value)
        {
            return new Parameter()
            {
                Values = new object[] { value },
                ParameterType = ParameterType.Route,
                Name = name,
            };
        }

        /// <summary>
        /// value should be an enumerable of one or more values
        /// </summary>
        /// <param name="name"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Parameter CreateHeaderParameter(string name, IEnumerable<object> values)
        {
            return new Parameter()
            {
                ParameterType = ParameterType.Header,
                Values = values,
                Name = name,
            };
        }

        /// <summary>
        /// value should be an enumerable of one or more values
        /// </summary>
        /// <param name="name"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Parameter CreateQueryParameter(string name, IEnumerable<object> values)
        {
            return new Parameter()
            {
                ParameterType = ParameterType.Query,
                Values = values,
                Name = name,
            };
        }

        public IEnumerable<T> GetAttributesChain<T>() where T : Attribute
        {
            var attributes = new List<T>();
            if (ParentParameter != null)
            {
                attributes.AddRange(ParentParameter.GetAttributesChain<T>());
            }

            if (SourceParameterInfo != null)
            {
                attributes.AddRange(SourceParameterInfo.GetCustomAttributes<T>());
            }

            return attributes;
        }

        internal static Parameter CreateContentParameter(ParameterInfo parameterInfo, object sourceValue)
        {
            return new Parameter()
            {
                SourceValue = sourceValue,
                SourceParameterInfo = parameterInfo,
                Values = new object[] { sourceValue },
                ParameterType = ParameterType.Content,
                Name = parameterInfo.Name,
            };
        }

        internal static Parameter CreateRouteParameter(ParameterInfo parameterInfo, object sourceValue)
        {
            return new Parameter()
            {
                SourceValue = sourceValue,
                SourceParameterInfo = parameterInfo,
                Values = new object[] { sourceValue },
                ParameterType = ParameterType.Route,
                Name = parameterInfo.Name,
            };
        }

        internal static Parameter CreateHeaderParameter(ParameterInfo parameterInfo, object sourceValue)
        {
            var values = IsEnumerable(sourceValue) ? ((IEnumerable)sourceValue).Cast<object>() : new object[] { sourceValue };
            return new Parameter()
            {
                SourceValue = sourceValue,
                SourceParameterInfo = parameterInfo,
                ParameterType = ParameterType.Header,
                Values = values,
                Name = parameterInfo.Name,
            };
        }

        internal static Parameter CreateQueryParameter(ParameterInfo parameterInfo, object sourceValue)
        {
            var values = IsEnumerable(sourceValue) ? ((IEnumerable)sourceValue).Cast<object>() : new object[] { sourceValue };
            return new Parameter()
            {
                SourceValue = sourceValue,
                SourceParameterInfo = parameterInfo,
                ParameterType = ParameterType.Query,
                Values = values,
                Name = parameterInfo.Name,
            };
        }

        private static bool IsEnumerable(object value) => value is IEnumerable && (value.GetType().GetTypeInfo().IsGenericType || value.GetType().IsArray);
    }
}