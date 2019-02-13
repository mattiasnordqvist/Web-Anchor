using System;

namespace WebAnchor.Attributes.Content
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormUrlEncodedPropertyAttribute : Attribute
    {
        public FormUrlEncodedPropertyAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; }
    }
}
