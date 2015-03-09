using System.IO;
using System.Net.Http;

namespace WebAnchor.ResponseParser
{
    public interface IContentDeserializer
    {
        T Deserialize<T>(StreamReader streamReader, HttpResponseMessage httpResponseMessage);
    }
}