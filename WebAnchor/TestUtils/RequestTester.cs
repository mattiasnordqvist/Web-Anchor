using System;
using System.Collections.Generic;
using System.Net.Http;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.TestUtils
{
    //public class RequestTester : IInterceptor
    //{
    //    private readonly Action<HttpRequestMessage> _assert;

    //    private readonly Action<IApiSettings> _configure;

    //    private readonly Action<IEnumerable<Parameter>, RequestTransformContext> _pipelineAction;

    //    private readonly DefaultApiSettings _settings;

    //    public RequestTester(Action<HttpRequestMessage> assert = null, Action<IApiSettings> configure = null, Action<IEnumerable<Parameter>, RequestTransformContext> pipelineAction = null, DefaultApiSettings settings = null)
    //    {
    //        _settings = settings ?? new DefaultApiSettings();
    //        _assert = assert ?? (a => { });
    //        _configure = configure ?? (a => { });
    //        _pipelineAction = pipelineAction ?? ((a, b) => { });
    //    }

    //    public void Intercept(IInvocation invocation)
    //    {
    //        _configure(_settings);
    //        _settings.Request.ParameterListTransformers.Add(new TestTransformer(_pipelineAction));
    //        var factory = new HttpRequestFactory(_settings);
    //        var httpRequest = factory.Create(invocation);
    //        _assert(httpRequest);
    //    }
    //}
}