using System.Collections.Generic;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.ResponseParser;

namespace WebAnchor.Tests.TestUtils
{
    public class TestSettings : ApiSettings
    {
        private IContentSerializer _contentSerializer;
        private IContentDeserializer _contentDeserializer;

        public TestSettings()
        {
            ListTransformers = base.CreateParameterListTransformers();
        }

        public IList<IParameterListTransformer> ListTransformers { get; protected set; }

        public TestSettings OverrideContentSerializer(IContentSerializer serializer)
        {
            _contentSerializer = serializer;
            return this;
        }

        public TestSettings OverrideContentDeserializer(IContentDeserializer deserializer)
        {
            _contentDeserializer = deserializer;
            return this;
        }

        public override IContentSerializer CreateContentSerializer()
        {
            return _contentSerializer ?? base.CreateContentSerializer();
        }

        public override IContentDeserializer CreateContentDeserializer()
        {
            return _contentDeserializer ?? base.CreateContentDeserializer();
        }

        public override IList<IParameterListTransformer> CreateParameterListTransformers()
        {
            return ListTransformers;
        }
    }
}