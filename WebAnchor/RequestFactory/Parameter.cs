using System;
using System.Reflection;

namespace WebAnchor.RequestFactory
{
    public class Parameter
    {
        public Parameter(Parameter parentParameter, object parameterValue)
        {
            ParentParameter = parentParameter;
            SourceParameterInfo = parentParameter.SourceParameterInfo;
            SourceValue = parameterValue;
            SourceType = parameterValue.GetType();
            ParameterType = parentParameter.ParameterType;
        }

        public Parameter(ParameterInfo parameterInfo, object parameterValue, ParameterType parameterType)
        {
            SourceParameterInfo = parameterInfo;
            SourceValue = parameterValue;
            ParameterType = parameterType;
            SourceType = parameterValue.GetType();
        }

        /// <summary>
        /// The type of the SourceValue.
        /// </summary>
        public Type SourceType { get; private set; }

        /// <summary>
        /// The source value from api method parameters.
        /// </summary>
        public object SourceValue { get; private set; }

        /// <summary>
        /// The parameterinfo of the SourceValue.
        /// </summary>
        public ParameterInfo SourceParameterInfo { get; }

        /// <summary>
        /// If this parameter was created from another parameter, that other parameter would be the parent parameter
        /// </summary>
        public Parameter ParentParameter { get; private set; }

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
        public object Value { get; set; }
    }
}