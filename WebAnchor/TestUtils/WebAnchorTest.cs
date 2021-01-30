using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.TestUtils
{
    public class WebAnchorTest
    {
        protected void TestTheRequest<T>(Action<T> apiCall, Action<HttpRequestMessage> assertHttpRequestMessage, Action<IApiSettings> configure = null, Action<IEnumerable<Parameter>, RequestTransformContext> assertParametersAndContext = null, DefaultApiSettings settings = null) where T : class
        {
            HttpClient client = new HttpClient(new TesterHandler(assertHttpRequestMessage));
            var _settings = settings ?? new DefaultApiSettings();
            if (configure != null) { configure(_settings); }
            var api = Api.For<T>(client, _settings);
            apiCall(api);
        }

        protected TReturn GetResponse<T, TReturn>(Func<T, TReturn> apiCall, HttpResponseMessage responseMessage, DefaultApiSettings settings = null)
            where T : class
        {
            HttpClient client = new HttpClient(new ReturnHandler(responseMessage));
            client.BaseAddress = new Uri("http://localhost");
            var _settings = settings ?? new DefaultApiSettings();
            var api = Api.For<T>(client, _settings);
            return apiCall(api);
        }
    }

    public class TesterHandler : DelegatingHandler
    {
        private readonly Action<HttpRequestMessage> _assertHttpRequestMessage;

        public TesterHandler(Action<HttpRequestMessage> assertHttpRequestMessage)
            : base()
        {
            _assertHttpRequestMessage = assertHttpRequestMessage;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _assertHttpRequestMessage(request);
            return Task.FromResult<HttpResponseMessage>(null);
        }
    }

    public class ReturnHandler : DelegatingHandler
    {
        private readonly HttpResponseMessage _responseMessage;

        public ReturnHandler(HttpResponseMessage responseMessage)
            : base()
        {
            _responseMessage = responseMessage;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult<HttpResponseMessage>(_responseMessage);
        }
    }
}