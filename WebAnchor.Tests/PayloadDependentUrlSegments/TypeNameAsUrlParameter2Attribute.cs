using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute.List;

namespace WebAnchor.Tests.PayloadDependentUrlSegments
{
    public class TypeNameAsUrlParameter2Attribute : ParameterListTransformerAttribute
    {
        public override IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext)
        {
            var list = parameters.ToList();

            if (requestTransformContext.MethodInfo.DeclaringType.GetGenericArguments().Any())
            {
                var value = requestTransformContext.MethodInfo.DeclaringType.GetGenericArguments().First().Name.ToLower();
                list.Add(new Parameter("type", value, ParameterType.Route));
            }

            return list;
        }
    }
}