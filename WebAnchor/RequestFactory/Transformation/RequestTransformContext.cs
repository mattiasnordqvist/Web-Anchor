using System.Collections.Generic;
using System.Linq;
using WebAnchor.RequestFactory.Serialization;
using WebAnchor.RequestFactory.ValueFormatting;

namespace WebAnchor.RequestFactory.Transformation
{
    public class RequestTransformContext
    {
        public RequestTransformContext(ApiInvocation apiInvocation, IApiSettings settings) 
        {
            ApiInvocation = apiInvocation;
            Data = settings.Data;
            ContentSerializer = settings.Request.ContentSerializer;
            ParameterValueFormatter = settings.Request.ParameterValueFormatter;
            InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl = settings.Request.InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl;
            EncodeUrlSegmentSeparatorsInUrlSegmentSubstitutions = settings.Request.EncodeUrlSegmentSeparatorsInUrlSegmentSubstitutions;
            ParameterListTransformers = settings.Request.ParameterListTransformers.ToList();
            QueryParameterListStrategy = settings.Request.QueryParameterListStrategy;
        }

        public ApiInvocation ApiInvocation { get; private set; }
        public IDictionary<string, object> Data { get; private set; }
        public string UrlTemplate { get; internal set; }
        public IContentSerializer ContentSerializer { get; set; }
        public IParameterValueFormatter ParameterValueFormatter { get; set; }
        public bool InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl { get; set; }
        public bool EncodeUrlSegmentSeparatorsInUrlSegmentSubstitutions { get; set; }
        public List<IParameterListTransformer> ParameterListTransformers { get; set; }
        public IQueryParamaterListStrategy QueryParameterListStrategy { get; set; }
    }
}