using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Castle.Core.Internal;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Default
{
    public class DefaultParameterTransformer : IParameterListTransformer
    {
        public void Resolve(Parameter parameter, ParameterTransformContext parameterTransformContext)
        {
            if (parameter.ParameterInfo != null)
            {
                parameter.Name = parameter.ParameterInfo.Name;    
            }

            if (parameter.ParameterType == ParameterType.Content)
            {
                parameter.Value = ShouldCreateDictionaryFromContent(parameter) ? parameter.ParameterValue.ToDictionary() : parameter.ParameterValue;
            }
            else
            {
                parameter.Value = parameter.ParameterValue?.ToString();
            }
        }

        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            foreach (var parameter in parameters)
            {
                Resolve(parameter, parameterTransformContext);
            }                
            return parameters;
        }

        public void ValidateApi(Type type)
        {
            foreach (var method in type.GetTypeInfo().GetMethods())
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
            if (parameter.ParameterInfo.GetFirstAttributeInChain<AsDictionaryAttribute>() != null)
            {
                return true;
            }

            return IsParameterDeclaredWithAsDictionary(parameter);
        }

        private bool IsParameterDeclaredWithAsDictionary(Parameter parameter)
        {
            if (parameter.ParameterValue.GetType().GetTypeInfo().GetCustomAttribute<AsDictionaryAttribute>() != null)
            {
                return true;
            }

            return parameter.ParentParameter != null && IsParameterDeclaredWithAsDictionary(parameter.ParentParameter);
        }
    }
}