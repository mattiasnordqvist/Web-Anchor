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
        
        public IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext)
        {
            foreach (var p in parameters)
            {
                yield return p;
            }

            yield return Parameter.CreateHeaderParameter(Name, new object[] { Value });
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
