using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute.List;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Default
{
    public class AttributesTransformer : IParameterListTransformer
    {
        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            var listTransformersOnInterface = parameterTransformContext.MethodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes<ParameterListTransformerAttribute>();
            var parameterTransformersOnInterface = parameterTransformContext.MethodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes<ParameterTransformerAttribute>();

            var listTransformersOnMethod = parameterTransformContext.MethodInfo.GetCustomAttributes<ParameterListTransformerAttribute>();
            var parameterTransformersOnMethod = parameterTransformContext.MethodInfo.GetCustomAttributes<ParameterTransformerAttribute>();

            IEnumerable<Parameter> transformedParameters = parameters.ToList();

            foreach (var transformer in listTransformersOnInterface)
            {
                transformedParameters = transformer.TransformParameters(transformedParameters, parameterTransformContext);
            }

            foreach (var transformer in parameterTransformersOnInterface)
            {
                foreach (var parameter in transformedParameters)
                {
                    transformer.Context = parameterTransformContext;
                    transformer.Apply(parameter);
                }
            }

            foreach (var transformer in listTransformersOnMethod)
            {
                transformedParameters = transformer.TransformParameters(transformedParameters, parameterTransformContext);
            }

            foreach (var transformer in parameterTransformersOnMethod)
            {
                foreach (var parameter in transformedParameters)
                {
                    transformer.Context = parameterTransformContext;
                    transformer.Apply(parameter);
                }
            }

            foreach (var parameter in transformedParameters)
            {
                var attributes = parameter.GetAttributesChain();
                foreach (var attribute in attributes)
                {
                    attribute.Context = parameterTransformContext;
                    attribute.Apply(parameter);
                }
            }

            return transformedParameters;
        }

        public void ValidateApi(Type type)
        {
            var listTransformersOnInterface = type.GetTypeInfo().GetCustomAttributes<ParameterListTransformerAttribute>();
            var parameterTransformersOnInterface = type.GetTypeInfo().GetCustomAttributes<ParameterTransformerAttribute>();

            foreach (var listTransformer in listTransformersOnInterface)
            {
                listTransformer.ValidateApi(type);
            }

            foreach (var listTransformer in parameterTransformersOnInterface)
            {
                listTransformer.ValidateApi(type, null, null);
            }

            foreach (var method in type.GetMethods())
            {
                var listTransformersOnMethod = method.GetCustomAttributes<ParameterListTransformerAttribute>();
                var parameterTransformersOnMethod = method.GetCustomAttributes<ParameterTransformerAttribute>();

                foreach (var listTransformer in listTransformersOnMethod)
                {
                    listTransformer.ValidateApi(type);
                }

                foreach (var listTransformer in parameterTransformersOnMethod)
                {
                    listTransformer.ValidateApi(type, method, null);
                }

                foreach (var parameter in method.GetParameters())
                {
                    var transformersOnParameter = parameter.GetCustomAttributes<ParameterTransformerAttribute>();
                    foreach (var transformer in transformersOnParameter)
                    {
                        transformer.ValidateApi(type, method, parameter);
                    }
                }
            }
        }
    }
}