using System.Reflection;

using Castle.DynamicProxy;

namespace WebAnchor.RequestFactory
{
    public class ApiInvocation
    {
        private readonly IInvocation _invocation;

        public ApiInvocation(IInvocation invocation)
        {
            _invocation = invocation;
        }

        public MethodInfo Method
        {
            get
            {
                return _invocation.Method;
            }
        }

        public object GetArgumentValue(int index)
        {
            return _invocation.GetArgumentValue(index);
        }
    }
}