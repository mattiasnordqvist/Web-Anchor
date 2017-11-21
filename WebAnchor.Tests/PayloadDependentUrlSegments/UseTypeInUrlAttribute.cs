using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute.List;

namespace WebAnchor.Tests.PayloadDependentUrlSegments
{
    public class UseTypeInUrlAttribute : ParameterListTransformerAttribute
    {
        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            var list = parameters.ToList();
            if (parameterTransformContext.MethodInfo.DeclaringType.GetGenericArguments().Any())
            {
                var value = parameterTransformContext.MethodInfo.DeclaringType.GetGenericArguments().First().Name.ToLower();
                list.Add(new Parameter(null, value, ParameterType.Route)
                             {
                                 Name = "type"
                             });
            }

            return list;
        }
    }
}