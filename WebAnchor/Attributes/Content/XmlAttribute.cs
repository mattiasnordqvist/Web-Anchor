using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using WebAnchor.Attributes.Content;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.RequestFactory.Serialization
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class XmlAttribute : ParameterTransformerAttribute
    {
        public override void Apply(Parameter parameter, RequestTransformContext requestTransformContext)
        {
            var xmlws = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false, NewLineOnAttributes = false, Encoding = Encoding.UTF8 };
            requestTransformContext.ContentSerializer = new XmlContentSerializer(xmlws);
        }
        public override void ValidateApi(Type type, MethodInfo method, ParameterInfo parameter)
        {
            if (!parameter.GetCustomAttributes(typeof(ContentAttribute), false).Any())
            {
                throw new WebAnchorException($"The attribute {nameof(XmlAttribute)} cannot be used on parameter {parameter.Name} because it does not have a {nameof(ContentAttribute)}");
            }
        }
    }
}
