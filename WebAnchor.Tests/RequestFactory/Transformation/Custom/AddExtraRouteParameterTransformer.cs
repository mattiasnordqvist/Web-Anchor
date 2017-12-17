using System;
using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Tests.RequestFactory.Transformation.Custom
{
    public class AddExtraRouteParameterTransformer : IParameterListTransformer
    {
        private readonly string _name;
        private readonly object _value;

        public AddExtraRouteParameterTransformer(string name, object value)
        {
            _name = name;
            _value = value;
        }

        public IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext)
        {
            var parameterList = parameters.ToList();
            parameterList.Add(Parameter.CreateRouteParameter(_name, _value));
            return parameterList;
        }

        public void ValidateApi(Type type)
        {
        }
    }
}