using System.Collections.Generic;
using WebAnchor.RequestFactory.Serialization;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.UrlNormalization;
using WebAnchor.RequestFactory.ValueFormatting;

namespace WebAnchor.RequestFactory
{
    public class DefaultApiRequestSettings : IApiRequestSettings
    {
        public DefaultApiRequestSettings()
        {
            ParameterListTransformers = new DefaultParameterListTransformers();
            InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl = true;
            EncodeUrlSegmentSeparatorsInUrlSegmentSubstitutions = false;
            ContentSerializer = new JsonContentSerializer(new Newtonsoft.Json.JsonSerializer());
            ParameterValueFormatter = new DefaultParameterValueFormatter();
            QueryParameterListStrategy = new NormalQueryParamaterListStrategy();
            UrlNormalizers = new List<IUrlNormalizer>();
        }

        public virtual List<IParameterListTransformer> ParameterListTransformers { get; set; }
        public virtual bool InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl { get; set; }
        public virtual bool EncodeUrlSegmentSeparatorsInUrlSegmentSubstitutions { get; set; }
        public virtual IContentSerializer ContentSerializer { get; set; }
        public virtual IParameterValueFormatter ParameterValueFormatter { get; set; }
        public virtual IQueryParamaterListStrategy QueryParameterListStrategy { get; set; }
        public virtual List<IUrlNormalizer> UrlNormalizers { get; set; }
    }
}