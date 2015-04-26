using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformers;

namespace WebAnchor.Tests.PayloadDependentUrlSegments
{
    public class TypeNameAsUrlParameterAttribute : ParameterTransformerAttribute
    {
        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            var list = parameters.ToList();
            var content = list.First(x => x.ParameterType == ParameterType.Content);
            list.Add(new Parameter(null, content.ParameterValue.GetType().Name.ToLower(), ParameterType.Route)
            {
                Name = "type"
            });
            return list;
        }
    }
}