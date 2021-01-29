using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAnchor.ResponseParser.ResponseHandlers
{
    public class AsyncDeserializingResponseHandler : IResponseHandler
    {
        protected readonly IContentDeserializer ContentDeserializer;

        public AsyncDeserializingResponseHandler(IContentDeserializer contentDeserializer)
        {
            ContentDeserializer = contentDeserializer;
        }

        public HttpCompletionOption HttpCompletionOptions => HttpCompletionOption.ResponseContentRead;

        public bool CanHandle(MethodInfo methodInfo)
        {
            return methodInfo.ReturnType.GetTypeInfo().IsGenericType && methodInfo.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
        }

        public async Task<T> HandleAsync<T>(HttpResponseMessage httpResponseMessage, MethodInfo methodInfo)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
                return ContentDeserializer.Deserialize<T>(stream, httpResponseMessage);
            }
            else
            {
                throw new ApiException(httpResponseMessage);
            }
        }
    }
}