using System.Reflection;

namespace WebAnchor.RequestFactory
{
    public class ApiInvocation
    {
        private readonly MethodBase _methodBase;
        private readonly object[] _parameters;

        public ApiInvocation(MethodBase methodBase, object[] parameters)
        {
            _methodBase = methodBase;
            this._parameters = parameters;
        }

        public MethodBase Method => _methodBase;

        public object GetArgumentValue(int index)
        {
            return _parameters[index];
        }
    }
}