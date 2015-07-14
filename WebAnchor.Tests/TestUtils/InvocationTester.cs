using System;
using System.Collections.Generic;
using System.Net.Http;

using Castle.DynamicProxy;

using Newtonsoft.Json;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Tests.TestUtils
{
    public class InvocationTester : IInterceptor
    {
        private readonly Action<HttpRequestMessage> _assert;

        private readonly Action<HttpRequestFactory> _configure;

        private readonly Action<IEnumerable<Parameter>, ParameterTransformContext> _pipelineAction;

        public InvocationTester(Action<HttpRequestMessage> assert = null, Action<HttpRequestFactory> configure = null, Action<IEnumerable<Parameter>, ParameterTransformContext> pipelineAction = null)
        {
            _assert = assert ?? (a => { });
            _configure = configure ?? (a => { });
            _pipelineAction = pipelineAction ?? ((a, b) => { });
        }

        public void Intercept(IInvocation invocation)
        {
            var listTransformers = new ApiSettings().CreateParameterListTransformers();
            listTransformers.Add(new TestTransformer(_pipelineAction));
            var factory = new HttpRequestFactory(new ContentSerializer(new JsonSerializer()), listTransformers);
            _configure(factory);
            var httpRequest = factory.Create(invocation);
            _assert(httpRequest);
        }
    }
}