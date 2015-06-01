using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;

using Castle.Core.Internal;

using Newtonsoft.Json;

using WebAnchor.ResponseParser;
using WebAnchor.Tests.IntegrationTests;

namespace WebAnchor.Tests.ProofOfConcepts.HeaderExtraction.Fixtures
{
    public class HeaderEnabledContentDeserializer : JsonContentDeserializer
    {
        public HeaderEnabledContentDeserializer(JsonSerializer jsonSerializer)
            : base(jsonSerializer)
        {
        }

        public override T Deserialize<T>(StreamReader streamReader, HttpResponseMessage message)
        {
            var t = base.Deserialize<T>(streamReader, message);
            GetPropertiesWithHeaderAttribute(t).ForEach(
                x => x.SetValue(t, message.Headers.GetValues(x.GetAttribute<HeaderAttribute>().Header).First()));
            return t;
        }

        private List<PropertyInfo> GetPropertiesWithHeaderAttribute<T>(T t)
        {
            return t.GetType().GetProperties().Where(x => x.HasAttribute<HeaderAttribute>()).ToList();
        }
    }
}