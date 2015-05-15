using System;
using System.Collections.Generic;
using System.Linq;

using Castle.Core.Internal;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Default
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

        public void ValidateApi(Type type)
        {
            foreach (var method in type.GetMethods())
            {
                if (method.GetParameters()
                    .Count(x => x.GetCustomAttributes(typeof(ContentAttribute), false).Any()) > 1)
                {
                    throw new WebAnchorException(string.Format("The method {0} in {1} cannot have more than one {2}", method.Name, method.DeclaringType.FullName, typeof(ContentAttribute).FullName));
                }
            }
        }
    }
}