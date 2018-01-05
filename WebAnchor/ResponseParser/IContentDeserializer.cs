using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAnchor.ResponseParser
{
    public interface IContentDeserializer
    {
        Task<object> Deserialize(Stream stream, Type t, HttpResponseMessage httpResponseMessage);
    }
}