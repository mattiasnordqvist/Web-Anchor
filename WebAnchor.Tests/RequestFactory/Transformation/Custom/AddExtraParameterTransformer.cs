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
        private readonly ParameterType _type;

        public AddExtraParameterTransformer(string name, object value, ParameterType type = ParameterType.Query)
        {
            _name = name;
            _value = value;
            _type = type;
        }

        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            var parameterList = parameters.ToList();
            parameterList.Add(new Parameter(null, _value, _type) { Name = _name, Value = _value });
            return parameterList;
        }
    }
}