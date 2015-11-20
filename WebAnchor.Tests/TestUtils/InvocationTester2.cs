using System.Net.Http;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace WebAnchor.Tests.TestUtils
{
    public class InvocationTester2 : IInterceptor
    {
        private readonly HttpResponseMessage _response;

        private readonly ApiSettings _settings;

        public InvocationTester2(HttpResponseMessage response, ApiSettings settings = null)
        {
            _response = response;
            _settings = settings ?? new ApiSettings();
        }

        public void Intercept(IInvocation invocation)
        {
             _settings.GetResponseParser().Parse(Task.FromResult(_response), invocation);
        }
    }
}