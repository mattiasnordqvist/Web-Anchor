using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace WebAnchor.ResponseParser.ResponseHandlers
{
    public class AsyncTaskHandler : IResponseHandler
    {
        public AsyncTaskHandler()
        {
        }

        public HttpCompletionOption HttpCompletionOptions => HttpCompletionOption.ResponseContentRead;

        public bool CanHandle(IInvocation invocation)
        {
            return !invocation.Method.ReturnType.GetTypeInfo().IsGenericType && invocation.Method.ReturnType == typeof(Task);
        }

        public void Handle(Task<HttpResponseMessage> task, IInvocation invocation)
        {
            invocation.ReturnValue = task.Then(httpResponseMessage =>
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return Task.CompletedTask;
                }
                else
                {
                    throw new ApiException(httpResponseMessage);
                }
            });
        }
    }
}