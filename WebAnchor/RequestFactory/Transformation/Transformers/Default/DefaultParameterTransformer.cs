using System;
using System.Collections.Generic;
using System.Linq;

using Castle.Core.Internal;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Default
{
    public class DefaultParameterTransformer : IParameterListTransformer
    {
        public void Resolve(Parameter parameter, ParameterTransformContext parameterTransformContext)
        {
            if (parameter.SourceParameterInfo != null)
            {
                parameter.Name = parameter.SourceParameterInfo.Name;    
            }

            if (parameter.ParameterType == ParameterType.Content)
            {
                parameter.Value = ShouldCreateDictionaryFromContent(parameter) ? parameter.SourceValue.ToDictionary() : parameter.SourceValue;
            }
            else
            {
                parameter.Value = parameter.SourceValue?.ToString();
            }
        }

        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            foreach(var parameter in parameters)
            {
                Resolve(parameter, parameterTransformContext);
                yield return parameter;
            }
        }

        public void ValidateApi(Type type)
        {
            foreach (var method in type.GetMethods())
            {
                if (method.GetParameters()
                    .Count(x => x.GetCustomAttributes(typeof(ContentAttribute), false).Any()) > 1)
                {
                    throw new WebAnchorException($"The method {method.Name} in {method.DeclaringType.FullName} cannot have more than one {typeof(ContentAttribute).FullName}");
                }
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
            if (parameter.SourceValue.GetType().GetAttribute<ContentAsDictionaryAttribute>() != null)
            {
                return true;
            }

            return parameter.ParentParameter != null && IsParameterDeclaredWithAsDictionary(parameter.ParentParameter);
        }
    }
}