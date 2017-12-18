using System.Net.Http;

namespace WebAnchor.RequestFactory.Serialization
{
    public interface IContentSerializer
    {
        HttpContent Serialize(object value, Parameter content);
    }
}