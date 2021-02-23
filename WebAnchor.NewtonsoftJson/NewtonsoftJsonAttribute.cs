using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using WebAnchor.Attributes.Content;
using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.NewtonsoftJson
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class NewtonsoftJsonAttribute : ParameterTransformerAttribute
    {
        public override void Apply(Parameter parameter, RequestTransformContext requestTransformContext)
        {
            requestTransformContext.ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializer());
        }

        public override void ValidateApi(Type type, MethodInfo method, ParameterInfo parameter)
        {
            if (!parameter.GetCustomAttributes(typeof(ContentAttribute), false).Any())
            {
                throw new WebAnchorException($"The attribute {nameof(NewtonsoftJsonAttribute)} cannot be used on parameter {parameter.Name} because it does not have a {nameof(ContentAttribute)}");
            }
        }
    }
}
