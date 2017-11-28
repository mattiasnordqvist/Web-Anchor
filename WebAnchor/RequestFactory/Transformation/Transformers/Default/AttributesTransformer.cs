using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute.List;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Default
{
    public class AttributesTransformer : IParameterListTransformer
    {
        public IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext)
        {
            var listTransformersOnInterface = requestTransformContext.ApiInvocation.Method.DeclaringType.GetTypeInfo().GetCustomAttributes<ParameterListTransformerAttribute>();
            var parameterTransformersOnInterface = requestTransformContext.ApiInvocation.Method.DeclaringType.GetTypeInfo().GetCustomAttributes<ParameterTransformerAttribute>();

            var listTransformersOnMethod = requestTransformContext.ApiInvocation.Method.GetCustomAttributes<ParameterListTransformerAttribute>();
            var parameterTransformersOnMethod = requestTransformContext.ApiInvocation.Method.GetCustomAttributes<ParameterTransformerAttribute>();

            IEnumerable<Parameter> transformedParameters = parameters.ToList();

            foreach (var transformer in listTransformersOnInterface)
            {
                transformedParameters = transformer.Apply(transformedParameters, requestTransformContext);
            }

            foreach (var transformer in parameterTransformersOnInterface)
            {
                foreach (var parameter in transformedParameters)
                {
                    transformer.Apply(parameter, requestTransformContext);
                }
            }

            foreach (var transformer in listTransformersOnMethod)
            {
                transformedParameters = transformer.Apply(transformedParameters, requestTransformContext);
            }

            foreach (var transformer in parameterTransformersOnMethod)
            {
                foreach (var parameter in transformedParameters)
                {
                    transformer.Apply(parameter, requestTransformContext);
                }
            }

            foreach (var parameter in transformedParameters)
            {
                var attributes = parameter.GetAttributesChain<ParameterTransformerAttribute>();
                foreach (var attribute in attributes)
                {
                    attribute.Apply(parameter, requestTransformContext);
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
                listTransformer.ValidateApi(type, null);
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
                    listTransformer.ValidateApi(type, method);
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