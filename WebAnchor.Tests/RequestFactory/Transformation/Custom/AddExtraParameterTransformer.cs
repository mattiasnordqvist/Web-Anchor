using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Tests.RequestFactory.Transformation.Custom
{
    public class AddExtraParameterTransformer : ParameterListTransformerBase
    {
        private readonly string _name;
        private readonly object _value;

        public AddExtraParameterTransformer(string name, object value)
        {
            _name = name;
            _value = value;
        }

        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            var parameterList = parameters.ToList();
            parameterList.Add(new Parameter(null, _value, ParameterType.Query) { Name = _name, Value = _value });
            return parameterList;
        }
    }
}