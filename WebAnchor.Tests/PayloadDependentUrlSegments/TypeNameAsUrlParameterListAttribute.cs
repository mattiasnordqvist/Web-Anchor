using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute.List;

namespace WebAnchor.Tests.PayloadDependentUrlSegments
{
    public class TypeNameAsUrlParameterListAttribute : ParameterListTransformerAttribute
    {
        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            var list = parameters.ToList();
            var content = list.First(x => x.ParameterType == ParameterType.Content);
            list.Add(new Parameter(null, content.SourceValue.GetType().Name.ToLower(), ParameterType.Route)
            {
                Name = "type"
            });

            return list;
        }
    }
}