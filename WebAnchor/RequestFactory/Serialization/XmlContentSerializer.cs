using System.Net.Http;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace WebAnchor.RequestFactory.Serialization
{
    internal class XmlContentSerializer : IContentSerializer
    {

        public virtual HttpContent Serialize(object value, Parameter content)
        {
            if (content == null)
            {
                return null;
            }

            var s1 = new XmlSerializer(content.SourceParameterInfo.ParameterType);
            var builder = new StringBuilder();
            var xmlws = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false, NewLineOnAttributes = false, Encoding = Encoding.UTF8 };
            using (var writer = XmlWriter.Create(builder, xmlws))
            {
                s1.Serialize(writer, value);
            }
            string xml = builder.ToString();

            return new StringContent(xml, Encoding.UTF8, "application/xml");

        }
    }
}