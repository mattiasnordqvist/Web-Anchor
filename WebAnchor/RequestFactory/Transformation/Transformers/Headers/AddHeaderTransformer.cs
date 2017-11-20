using System.Collections.Generic;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Headers
{
    public class AddHeaderTransformer : ParameterListTransformerBase
    {
        public AddHeaderTransformer(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; private set; }
        
        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            foreach (var p in parameters)
            {
                yield return p;
            }

            yield return new Parameter(null, Value, ParameterType.Header)
            {
                Name = Name,
                Value = Value
            };
        }

        protected virtual void SetValue(string value)
        {
            Value = value;
        }
    }
}
