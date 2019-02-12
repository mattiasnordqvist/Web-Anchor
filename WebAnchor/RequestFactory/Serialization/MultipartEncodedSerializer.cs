using System.Collections.Generic;
using System.Net.Http;

namespace WebAnchor.RequestFactory.Serialization
{
    public class MultipartEncodedSerializer : IContentSerializer
    {
        public virtual HttpContent Serialize(object value, Parameter content)
        {
            if (content == null)
            {
                return null;
            }

            var form = new MultipartFormDataContent();

            foreach (var part in GetContentParts(value))
            {
                form.Add(part.CreateContent());
            }

            return form;
        }

        protected IEnumerable<ContentPartBase> GetContentParts(object value)
        {
            switch (value)
            {
                case ContentPartBase singleContentPart:
                    return new[] { singleContentPart };
                case IEnumerable<ContentPartBase> enumerableContentParts:
                    return enumerableContentParts;
            }

            throw new WebAnchorException($"Unexpected parameter type {value.GetType()} when serializing multipart data - only types deriving from {nameof(ContentPartBase)} or IEnumerable<{nameof(ContentPartBase)}> are supported");
        }
    }
}