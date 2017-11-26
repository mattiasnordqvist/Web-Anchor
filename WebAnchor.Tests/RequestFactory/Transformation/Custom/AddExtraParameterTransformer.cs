using System;
using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Tests.RequestFactory.Transformation.Custom
{
    public class AddExtraParameterTransformer : IParameterListTransformer
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

        public IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext)
        {
            var parameterList = parameters.ToList();
            parameterList.Add(new Parameter(_name, _value, _type));
            return parameterList;
        }

        public void ValidateApi(Type type)
        {
        }
    }
}