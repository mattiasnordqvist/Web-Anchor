using System;
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
            return invocation.Method.ReturnType.GetTypeInfo().IsGenericType && invocation.Method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
        }

        public void Handle(Task<HttpResponseMessage> httpResponseMessage, IInvocation invocation)
        {
            var genericArgument = invocation.Method.ReturnType.GetGenericArguments()[0];
            const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic;

            var method = typeof(AsyncDeserializingResponseHandler).GetMethod("Convert", Flags).MakeGenericMethod(genericArgument);
            invocation.ReturnValue = method.Invoke(this, new[] { InternalDeserialize(httpResponseMessage, genericArgument) });
        }

        private Task<object> InternalDeserialize(Task<HttpResponseMessage> task, Type type)
        {
            return task.Then(httpResponseMessage =>
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return httpResponseMessage.Content.ReadAsStreamAsync()
                      .Then(stream => ContentDeserializer.Deserialize(stream, type, httpResponseMessage));
                }
                else
                {
                    throw new ApiException(httpResponseMessage);
                }
            });
        }

        private Task<T> Convert<T>(Task<object> task)
        {
            TaskCompletionSource<T> res = new TaskCompletionSource<T>();

            return task.ContinueWith(t =>
            {
                if (t.IsCanceled)
                {
                    res.TrySetCanceled();
                }
                else if (t.IsFaulted)
                {
                    res.TrySetException(t.Exception.InnerExceptions);
                }
                else
                {
                    res.TrySetResult((T)t.Result);
                }

                return res.Task;
            }, TaskContinuationOptions.ExecuteSynchronously).Unwrap();
        }
    }
}