using System;
using System.Reflection;

namespace WebAnchor.RequestFactory
{
    public class Parameter
    {
        public Parameter(Parameter parentParameter, object value)
        {
            ParentParameter = parentParameter;
            ParameterInfo = parentParameter.ParameterInfo;
            ParameterValue = value;
            Type = value.GetType();
            ParameterType = parentParameter.ParameterType;
        }

        public Parameter(ParameterInfo parameterInfo, object value, ParameterType parameterType)
        {
            ParameterInfo = parameterInfo;
            ParameterValue = value;
            ParameterType = parameterType;
            Type = value.GetType();
        }

        public object ParameterValue { get; private set; }
        public ParameterInfo ParameterInfo { get; private set; }
        public Parameter ParentParameter { get; private set; }
        public ParameterType ParameterType { get; private set; }
        public Type Type { get; private set; }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}