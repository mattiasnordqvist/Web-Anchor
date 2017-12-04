using System;
using System.Linq;
using System.Reflection;
using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Attributes.Content
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ContentAttribute : ParameterTransformerAttribute
    {
        public ContentAttribute()
        {
        }

        public override void Apply(Parameter parameter, RequestTransformContext requestTransformContext)
        {
        }

        public override void ValidateApi(Type type, MethodInfo method, ParameterInfo parameter)
        {
            if (method.GetParameters().Count(x => x.GetCustomAttributes(typeof(ContentAttribute), false).Any()) > 1)
            {
                throw new WebAnchorException($"The method {method.Name} in {type.FullName} cannot have more than one {typeof(ContentAttribute).FullName}");
            }
        }
    }
}
