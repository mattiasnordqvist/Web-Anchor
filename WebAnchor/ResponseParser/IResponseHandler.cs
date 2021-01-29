using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAnchor.ResponseParser
{
    public interface IResponseHandler
    {
        HttpCompletionOption HttpCompletionOptions { get; }

        bool CanHandle(MethodInfo methodInfo);

        Task<T> HandleAsync<T>(HttpResponseMessage httpResponseMessage, MethodInfo methodInfo);
    }
}