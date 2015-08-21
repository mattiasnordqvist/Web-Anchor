using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace WebAnchor.ResponseParser.ResponseHandlers
{
    public class AsyncDeserializingResponseHandler : IResponseHandler
    {
        protected readonly IContentDeserializer ContentDeserializer;

        public AsyncDeserializingResponseHandler(IContentDeserializer contentDeserializer)
        {
            ContentDeserializer = contentDeserializer;
        }

        public bool CanHandle(Task<HttpResponseMessage> httpResponseMessage, IInvocation invocation)
        {
            return invocation.Method.ReturnType.IsGenericType && invocation.Method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
        }

        public object Handle(Task<HttpResponseMessage> httpResponseMessage, IInvocation invocation)
        {
            var genericArgument = invocation.Method.ReturnType.GetGenericArguments()[0];

            const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic;

            var method = typeof(AsyncDeserializingResponseHandler).GetMethod("InternalDeserialize", Flags).MakeGenericMethod(genericArgument);

            return method.Invoke(this, new[] { httpResponseMessage });
        }

        private async Task<T> InternalDeserialize<T>(Task<HttpResponseMessage> task)
        {
            var httpResponseMessage = await task;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using (var tr = new StreamReader(await httpResponseMessage.Content.ReadAsStreamAsync()))
                {
                    return ContentDeserializer.Deserialize<T>(tr, task.Result);
                }
            }
            else
            {
                throw new ApiException(httpResponseMessage);
            }
        }
    }
}