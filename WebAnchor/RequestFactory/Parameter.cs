using System;
using System.Collections.Generic;
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
        public IEnumerable<object> SourceValues { get; private set; }

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

        public static Parameter CreateContentParameter(string name, object value)
        {
            return new Parameter()
            {
                Values = new object[] { value },
                ParameterType = ParameterType.Content,
                Name = name,
            };
        }

        public static Parameter CreateRouteParameter(string name, object value)
        {
            return new Parameter()
            {
                Values = new object[] { value },
                ParameterType = ParameterType.Route,
                Name = name,
            };
        }

        public static Parameter CreateHeaderParameter(string name, IEnumerable<object> values)
        {
            return new Parameter()
            {
                ParameterType = ParameterType.Header,
                Values = values,
                Name = name,
            };
        }

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

        internal static Parameter CreateContentParameter(ParameterInfo parameterInfo, object value)
        {
            return new Parameter()
            {
                SourceParameterInfo = parameterInfo,
                Values = new object[] { value },
                ParameterType = ParameterType.Content,
                Name = parameterInfo.Name,
            };
        }

        internal static Parameter CreateRouteParameter(ParameterInfo parameterInfo, object value)
        {
            return new Parameter()
            {
                SourceParameterInfo = parameterInfo,
                Values = new object[] { value },
                ParameterType = ParameterType.Route,
                Name = parameterInfo.Name,
            };
        }

        internal static Parameter CreateHeaderParameter(ParameterInfo parameterInfo, IEnumerable<object> values)
        {
            return new Parameter()
            {
                SourceParameterInfo = parameterInfo,
                ParameterType = ParameterType.Header,
                Values = values,
                Name = parameterInfo.Name,
            };
        }

        internal static Parameter CreateQueryParameter(ParameterInfo parameterInfo, IEnumerable<object> values)
        {
            return new Parameter()
            {
                SourceParameterInfo = parameterInfo,
                ParameterType = ParameterType.Query,
                Values = values,
                Name = parameterInfo.Name,
            };
        }
    }
}