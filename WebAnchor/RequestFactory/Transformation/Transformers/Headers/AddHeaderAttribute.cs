using System;
using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory.Transformation.Transformers.Attribute.List;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Headers
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = true)]
    public class AddHeaderAttribute : ParameterListTransformerAttribute
    {
        public AddHeaderAttribute(string headerName, string value)
        {
            HeaderName = headerName;
            Value = value;
        }

        public string HeaderName { get; set; }
        public string Value { get; set; }

        public override IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext)
        {
            var p = parameters.ToList();
            p.Add(Parameter.CreateHeaderParameter(HeaderName, new[] { Value }));
            return p;
        }
    }
}