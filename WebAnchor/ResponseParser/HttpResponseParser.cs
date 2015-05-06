using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace WebAnchor.ResponseParser
{
    public class HttpResponseParser : IHttpResponseParser
    {
        protected readonly IContentDeserializer ContentDeserializer;

        public HttpResponseParser(IContentDeserializer contentDeserializer)
        {
            ContentDeserializer = contentDeserializer;
        }

        public virtual void ValidateApi(Type type)
        {
            foreach (var method in type.GetMethods())
            {
                if (!(method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>)))
                {
                    throw new WebAnchorException(string.Format("Return type of method {0} in {1} must be Task<HttpResponseMessage> or Task<T>", method.Name, method.DeclaringType.FullName));
                }
            }
        }

        public virtual void Parse(Task<HttpResponseMessage> httpResponseMessage, IInvocation invocation)
        {
            if (invocation.Method.ReturnType == typeof(Task<HttpResponseMessage>))
            {
                invocation.ReturnValue = httpResponseMessage;
            }
            else
            {
                var genericArgument = invocation.Method.ReturnType.GetGenericArguments()[0];

                const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic;

                var method = typeof(HttpResponseParser).GetMethod("InternalDeserialize", Flags).MakeGenericMethod(genericArgument);

                invocation.ReturnValue = method.Invoke(this, new[] { httpResponseMessage });
            }
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