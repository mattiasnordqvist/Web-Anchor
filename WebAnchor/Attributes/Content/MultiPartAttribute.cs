using System;
using System.Collections.Generic;
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
            if (!IsSupportedParameterType(parameter))
            {
                throw new WebAnchorException($"The attribute {nameof(MultipartAttribute)} can only be applied to properties that are of type deriving from {nameof(ContentPartBase)} or IEnumerable<{nameof(ContentPartBase)}>");
            }

            if (!parameter.GetCustomAttributes(typeof(ContentAttribute), false).Any())
            {
                throw new WebAnchorException($"The attribute {nameof(MultipartAttribute)} cannot be used on parameter {parameter.Name} because it does not have a {nameof(ContentAttribute)}");
            }
        }

        private static bool IsSupportedParameterType(ParameterInfo parameter)
        {
            return typeof(IEnumerable<ContentPartBase>).IsAssignableFrom(parameter.ParameterType) || 
                typeof(ContentPartBase).IsAssignableFrom(parameter.ParameterType);
        }
    }
}