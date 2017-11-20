using System;
using System.Collections.Generic;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute.List;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Headers.Dynamic
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface)]
    public class AddHeaderAttribute : ParameterListTransformerAttribute
    {
        private AddHeaderTransformer _transformer;

        public AddHeaderAttribute(string name, string value)
        {
            _transformer = new AddHeaderTransformer(name, value);
        }

        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            return _transformer.TransformParameters(parameters, parameterTransformContext);
        }


    }
}