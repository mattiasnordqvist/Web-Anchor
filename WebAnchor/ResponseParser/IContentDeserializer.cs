using System.IO;
using System.Net.Http;

namespace WebAnchor.ResponseParser
{
    public interface IContentDeserializer
    {
        T Deserialize<T>(Stream stream,  HttpResponseMessage httpResponseMessage);
    }
}