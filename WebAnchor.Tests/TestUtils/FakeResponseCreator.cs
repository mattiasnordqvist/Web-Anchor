using System.Net.Http;
using System.Threading.Tasks;

using Castle.DynamicProxy;
using WebAnchor.ResponseParser;

namespace WebAnchor.Tests.TestUtils
{
    public class FakeResponseCreator : IInterceptor
    {
        private readonly HttpResponseMessage _response;

        private readonly IApiSettings _settings;

        public FakeResponseCreator(HttpResponseMessage response, IApiSettings settings = null)
        {
            _response = response;
            _settings = settings ?? new DefaultApiSettings();
        }

        public void Intercept(IInvocation invocation)
        {
             new HttpResponseParser(_settings).Parse(Task.FromResult(_response), invocation);
        }
    }
}