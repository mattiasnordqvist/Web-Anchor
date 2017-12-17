using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute.List;

namespace WebAnchor.Tests.PayloadDependentUrlSegments
{
    public class UseTypeInUrlAttribute : ParameterListTransformerAttribute
    {
        public override IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext)
        {
            var list = parameters.ToList();
            if (requestTransformContext.ApiInvocation.Method.DeclaringType.GetGenericArguments().Any())
            {
                var value = requestTransformContext.ApiInvocation.Method.DeclaringType.GetGenericArguments().First().Name.ToLower();
                list.Add(Parameter.CreateRouteParameter("type", value));
            }

            return list;
        }
    }
}