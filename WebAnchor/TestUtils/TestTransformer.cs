using System;
using System.Collections.Generic;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.TestUtils
{
    public class TestTransformer : IParameterListTransformer
    {
        private readonly Action<IEnumerable<Parameter>, RequestTransformContext> _testAction;

        public TestTransformer(Action<IEnumerable<Parameter>, RequestTransformContext> testAction)
        {
            _testAction = testAction;
        }

        public IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext)
        {
            _testAction(parameters, requestTransformContext);
            return parameters;
        }

        public void ValidateApi(Type type)
        {
        }
    }
}