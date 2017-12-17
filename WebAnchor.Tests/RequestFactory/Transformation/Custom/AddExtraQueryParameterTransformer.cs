using System;
using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Tests.RequestFactory.Transformation.Custom
{
    public class AddExtraQueryParameterTransformer : IParameterListTransformer
    {
        private readonly string _name;
        private readonly IEnumerable<object> _values;

        public AddExtraQueryParameterTransformer(string name, IEnumerable<object> values)
        {
            _name = name;
            _values = values;
        }

        public IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext)
        {
            var parameterList = parameters.ToList();
            parameterList.Add(Parameter.CreateQueryParameter(_name, _values));
            return parameterList;
        }

        public void ValidateApi(Type type)
        {
        }
    }
}