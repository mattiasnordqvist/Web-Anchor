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
            var payload = list.First(x => x.ParameterType == ParameterType.Payload);
            list.Add(new Parameter(null, payload.ParameterValue.GetType().Name.ToLower(), ParameterType.Route)
            {
                Name = "payloadType"
            });
            return list;
        }
    }
}