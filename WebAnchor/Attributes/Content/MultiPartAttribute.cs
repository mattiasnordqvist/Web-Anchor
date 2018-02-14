using System;
using System.Linq;
using System.Reflection;
using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Serialization;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Attributes.Content
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class MultipartAttribute : ParameterTransformerAttribute
    {
        public override void Apply(Parameter parameter, RequestTransformContext requestTransformContext)
        {
            requestTransformContext.ContentSerializer = new MultipartEncodedSerializer();
        }

        public override void ValidateApi(Type type, MethodInfo method, ParameterInfo parameter)
        {
            if (parameter.ParameterType != typeof(MultipartContentData))
            {
                throw new WebAnchorException($"The attribute {nameof(MultipartAttribute)} can only be applied to properties of type {nameof(MultipartContentData)}");
            }

            if (!parameter.GetCustomAttributes(typeof(ContentAttribute), false).Any())
            {
                throw new WebAnchorException($"The attribute {nameof(MultipartAttribute)} cannot be used on parameter {parameter.Name} because it does not have a {nameof(ContentAttribute)}");
            }
        }
    }
}