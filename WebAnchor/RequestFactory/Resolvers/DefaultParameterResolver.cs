using System.Collections.Generic;

using Castle.Core.Internal;

namespace WebAnchor.RequestFactory.Resolvers
{
    public class DefaultParameterResolver : IParameterListTransformer
    {
        public void Resolve(Parameter parameter)
        {
            if (parameter.ParameterInfo != null)
            {
                parameter.Name = parameter.ParameterInfo.Name;    
            }

            if (parameter.ParameterType == ParameterType.Content)
            {
                parameter.Value = parameter.ParameterValue.ToDictionary();
            }
            else
            {
                parameter.Value = parameter.ParameterValue == null ? null : parameter.ParameterValue.ToString();
            }
        }

        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            parameters.ForEach(Resolve);
            return parameters;
        }
    }
}