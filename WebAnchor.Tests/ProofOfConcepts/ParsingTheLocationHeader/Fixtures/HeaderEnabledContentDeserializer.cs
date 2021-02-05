using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

using Castle.Core.Internal;


using WebAnchor.ResponseParser;
using WebAnchor.Tests.IntegrationTests;

namespace WebAnchor.Tests.ProofOfConcepts.ParsingTheLocationHeader.Fixtures
{
    public class HeaderEnabledContentDeserializer : JsonContentDeserializer
    {
        public HeaderEnabledContentDeserializer(JsonSerializerOptions options = null)
            : base(options)
        {
        }

        public override async Task<T> Deserialize<T>(Stream stream, HttpResponseMessage message)
        {
            var x = await base.Deserialize<T>(stream, message);
            GetPropertiesWithHeaderAttribute(x)
                .ForEach(y => y.SetValue(x, message.Headers.GetValues(y.GetAttribute<HeaderAttribute>().Header).First()));
            return x;
        }

        private List<PropertyInfo> GetPropertiesWithHeaderAttribute<T>(T t)
        {
            return t.GetType().GetProperties().Where(x => x.GetAttribute<HeaderAttribute>() != null).ToList();
        }
    }
}