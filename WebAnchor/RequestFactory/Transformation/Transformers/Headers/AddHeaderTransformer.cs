using System.Collections.Generic;
using System.Linq;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Headers
{
    public class AddHeaderTransformer : ParameterListTransformerBase
    {
        public AddHeaderTransformer(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public AddHeaderTransformer(string name, IEnumerable<string> values)
        {
            Name = name;
            Value = values;
        }

        public string Name { get; set; }
        public object Value { get; private set; }
        
        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            var parametersList = parameters.ToList();
            parametersList.Add(new Parameter(null, Value, ParameterType.Header)
            {
                Name = Name,
                Value = Value
            });
            return parametersList;
        }

        protected virtual void SetValue(string value)
        {
            Value = value;
        }

        protected virtual void SetValues(IEnumerable<string> values)
        {
            Value = values;
        }
    }
}
