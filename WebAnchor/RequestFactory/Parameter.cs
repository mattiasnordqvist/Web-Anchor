using System;
using System.Reflection;

namespace WebAnchor.RequestFactory
{
    public class Parameter
    {
        public Parameter(Parameter parentParameter, object parameterValue)
        {
            ParentParameter = parentParameter;
            ParameterInfo = parentParameter.ParameterInfo;
            ParameterValue = parameterValue;
            Type = parameterValue.GetType();
            ParameterType = parentParameter.ParameterType;
        }

        public Parameter(ParameterInfo parameterInfo, object parameterValue, ParameterType parameterType)
        {
            ParameterInfo = parameterInfo;
            ParameterValue = parameterValue;
            ParameterType = parameterType;
            Type = parameterValue.GetType();
        }

        public object ParameterValue { get; private set; }
        public ParameterInfo ParameterInfo { get; private set; }
        public Parameter ParentParameter { get; private set; }
        public ParameterType ParameterType { get; set; }
        public Type Type { get; private set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}