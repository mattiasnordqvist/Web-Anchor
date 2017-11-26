using System;
using System.Collections.Generic;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Tests.TestUtils
{
    public class TestTransformer : IParameterListTransformer
    {
        private readonly Action<IEnumerable<Parameter>, RequestTransformContext> _testAction;

        public TestTransformer(Action<IEnumerable<Parameter>, RequestTransformContext> testAction)
        {
            _testAction = testAction;
        }

        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, RequestTransformContext parameterTransformContext)
        {
            _testAction(parameters, parameterTransformContext);
            return parameters;
        }

        public void ValidateApi(Type type)
        {
        }
    }
}