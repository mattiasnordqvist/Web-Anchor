using System.Net.Http;

namespace WebAnchor.RequestFactory.Serialization
{
    public class MultipartEncodedSerializer : IContentSerializer
    {
        public HttpContent Serialize(object value, Parameter content)
        {
            if (content == null)
            {
                return null;
            }

            if (!(value is MultipartContentData multipartContentData))
            {
                throw new WebAnchorException($"Unexpected parameter type {value.GetType()} when serializing multipart data - only {nameof(MultipartContentData)} is supported");
            }

            var form = new MultipartFormDataContent();

            foreach (var part in multipartContentData.Parts)
            {
                form.Add(part.CreateContent());
            }

            return form;
        }
    }
}