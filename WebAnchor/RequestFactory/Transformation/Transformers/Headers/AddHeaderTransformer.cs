using System;
using System.Collections.Generic;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Headers
{
    public class AddHeaderTransformer : IParameterListTransformer
    {
        public AddHeaderTransformer(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; private set; }
        
        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, RequestTransformContext parameterTransformContext)
        {
            foreach (var p in parameters)
            {
                yield return p;
            }

            yield return new Parameter(Name, Value, ParameterType.Header);
        }

        public void ValidateApi(Type type)
        {
        }

        protected virtual void SetValue(string value)
        {
            Value = value;
        }
    }
}
