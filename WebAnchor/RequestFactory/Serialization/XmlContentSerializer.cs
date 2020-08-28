using System.Net.Http;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace WebAnchor.RequestFactory.Serialization
{
    public class XmlContentSerializer : IContentSerializer
    {
        public XmlContentSerializer(XmlWriterSettings settings)
        {
            Settings = settings;
        }

        public XmlWriterSettings Settings { get; }

        public virtual HttpContent Serialize(object value, Parameter content)
        {
            if (content == null)
            {
                return null;
            }

            var s1 = new XmlSerializer(content.SourceParameterInfo.ParameterType);
            var builder = new StringBuilder();
            using (var writer = XmlWriter.Create(builder, Settings))
            {
                s1.Serialize(writer, value);
            }
            string xml = builder.ToString();

            return new StringContent(xml, Encoding.UTF8, "application/xml");

        }
    }
}