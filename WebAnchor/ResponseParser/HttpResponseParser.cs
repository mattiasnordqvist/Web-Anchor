using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace WebAnchor.ResponseParser
{
    public class HttpResponseParser : IHttpResponseParser
    {
        private readonly IList<IResponseHandler> _responseHandlers;

        public HttpResponseParser(IList<IResponseHandler> responseHandlers)
        {
            _responseHandlers = responseHandlers;
        }

        public virtual void ValidateApi(Type type)
        {
        }

        public virtual void Parse(Task<HttpResponseMessage> httpResponseMessage, IInvocation invocation)
        {
            var handler = _responseHandlers.First(x => x.CanHandle(httpResponseMessage, invocation));
            if (handler == null)
            {
                throw new WebAnchorException(string.Format("Return type of method {0} in {1} cannot be handled by any of the registered response handlers.", invocation.Method.Name, invocation.Method.DeclaringType.FullName));
            }
            else
            {
                var result = handler.Handle(httpResponseMessage, invocation);
                invocation.ReturnValue = result;
            }
        }
    }
}