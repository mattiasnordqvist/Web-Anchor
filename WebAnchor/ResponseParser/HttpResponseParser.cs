using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebAnchor.ResponseParser
{
    public class HttpResponseHandlersList
    {
        private readonly List<IResponseHandler> _responseHandlers;

        public HttpResponseHandlersList(IApiSettings settings)
        {
            _responseHandlers = settings.Response.ResponseHandlers;
        }

        public virtual void ValidateApi(Type type)
        {
        }

        public IResponseHandler FindHandler(MethodInfo methodInfo)
        {
            var handler = _responseHandlers.First(x => x.CanHandle(methodInfo));
            if (handler == null)
            {
                throw new WebAnchorException($"Return type of method {methodInfo.Name} in {methodInfo.DeclaringType.FullName} cannot be handled by any of the registered response handlers.");
            }
            else
            {
                return handler;
            }
        }
    }
}