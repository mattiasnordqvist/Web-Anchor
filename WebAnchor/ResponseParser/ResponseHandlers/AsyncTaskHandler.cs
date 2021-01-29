using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAnchor.ResponseParser.ResponseHandlers
{
    public class AsyncTaskHandler : IResponseHandler
    {
        public AsyncTaskHandler()
        {
        }

        public HttpCompletionOption HttpCompletionOptions => HttpCompletionOption.ResponseContentRead;

        public bool CanHandle(MethodInfo methodInfo)
        {
            return !methodInfo.ReturnType.GetTypeInfo().IsGenericType && methodInfo.ReturnType == typeof(Task);
        }

        public Task<T> HandleAsync<T>(HttpResponseMessage httpResponseMessage, MethodInfo methodInfo)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new ApiException(httpResponseMessage);
            }
            return Task.FromResult(default(T));
        }
    }
}