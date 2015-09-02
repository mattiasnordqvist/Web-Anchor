using System;
using System.Collections.Generic;
using System.Net.Http;

using Castle.DynamicProxy;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Tests.TestUtils
{
    public class InvocationTester : IInterceptor
    {
        private readonly Action<HttpRequestMessage> _assert;

        private readonly Action<HttpRequestFactory> _configure;

        private readonly Action<IEnumerable<Parameter>, ParameterTransformContext> _pipelineAction;

        private readonly ApiSettings _settings;

        public InvocationTester(Action<HttpRequestMessage> assert = null, Action<HttpRequestFactory> configure = null, Action<IEnumerable<Parameter>, ParameterTransformContext> pipelineAction = null, ApiSettings settings = null)
        {
            _settings = settings ?? new ApiSettings();
            _assert = assert ?? (a => { });
            _configure = configure ?? (a => { });
            _pipelineAction = pipelineAction ?? ((a, b) => { });
        }

        public void Intercept(IInvocation invocation)
        {
            _settings.ParameterListTransformers.Add(new TestTransformer(_pipelineAction));
            var factory = (HttpRequestFactory)_settings.GetRequestFactory();
            _configure(factory);
            var httpRequest = factory.Create(invocation);
            _assert(httpRequest);
        }
    }
}