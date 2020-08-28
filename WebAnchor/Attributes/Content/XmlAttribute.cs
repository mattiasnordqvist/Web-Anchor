using System;
using System.Linq;
using System.Reflection;
using WebAnchor.Attributes.Content;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.RequestFactory.Serialization
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class XmlWebAnchorAttribute : ParameterTransformerAttribute
    {
        public override void Apply(Parameter parameter, RequestTransformContext requestTransformContext)
        {
            requestTransformContext.ContentSerializer = new XmlContentSerializer();
        }
        public override void ValidateApi(Type type, MethodInfo method, ParameterInfo parameter)
        {
            if (!parameter.GetCustomAttributes(typeof(ContentAttribute), false).Any())
            {
                throw new WebAnchorException($"The attribute {nameof(XmlWebAnchorAttribute)} cannot be used on parameter {parameter.Name} because it does not have a {nameof(ContentAttribute)}");
            }
        }
    }
}
