using System;
using System.Linq;
using System.Reflection;
using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers.Default;

namespace WebAnchor.Attributes.Content
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ContentAttribute : ParameterTransformerAttribute
    {
        public ContentAttribute(ContentType type = ContentType.Json)
        {
            Type = type;
        }

        public enum ContentType
        {
            Json,
            FormUrlEncoded
        }

        public ContentType Type { get; set; }

        public override void Apply(Parameter parameter)
        {
            if (parameter.ParameterType == ParameterType.Content)
            {
                parameter.Value = ShouldCreateDictionaryFromContent(parameter) ? parameter.SourceValue.ToDictionary() : parameter.SourceValue;
            }
        }

        public override void ValidateApi(Type type, MethodInfo method, ParameterInfo parameter)
        {
            if (method.GetParameters().Count(x => x.GetCustomAttributes(typeof(ContentAttribute), false).Any()) > 1)
            {
                throw new WebAnchorException($"The method {method.Name} in {method.DeclaringType.FullName} cannot have more than one {typeof(ContentAttribute).FullName}");
            }
        }

        private bool ShouldCreateDictionaryFromContent(Parameter parameter)
        {
            if (parameter.SourceParameterInfo.GetFirstAttributeInChain<ContentAsDictionaryAttribute>() != null)
            {
                return true;
            }

            return IsParameterDeclaredWithAsDictionary(parameter);
        }

        private bool IsParameterDeclaredWithAsDictionary(Parameter parameter)
        {
            if (parameter.SourceValue.GetType().GetTypeInfo().GetCustomAttribute<ContentAsDictionaryAttribute>() != null)
            {
                return true;
            }

            return parameter.ParentParameter != null && IsParameterDeclaredWithAsDictionary(parameter.ParentParameter);
        }
    }
}
