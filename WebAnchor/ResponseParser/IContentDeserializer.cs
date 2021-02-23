using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAnchor.ResponseParser
{
    public interface IContentDeserializer
    {
        ValueTask<T> Deserialize<T>(Stream stream,  HttpResponseMessage httpResponseMessage);
    }
}