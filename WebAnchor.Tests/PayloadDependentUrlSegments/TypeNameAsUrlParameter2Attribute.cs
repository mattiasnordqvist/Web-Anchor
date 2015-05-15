using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute.List;
using WebAnchor.RequestFactory.Transformers;

namespace WebAnchor.Tests.PayloadDependentUrlSegments
{
    public class TypeNameAsUrlParameter2Attribute : ParameterListTransformerAttribute
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