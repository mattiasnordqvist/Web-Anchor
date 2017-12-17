using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAnchor.RequestFactory.Transformation
{
    public class RequestTransformContext
    {
        public RequestTransformContext(ApiInvocation apiInvocation, IApiSettings settings) 
        {
            ApiInvocation = apiInvocation;
            ContentSerializer = settings.Request.ContentSerializer;
            ParameterValueToString = settings.Request.ParameterValueToString;
            InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl = settings.Request.InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl;
            TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators = settings.Request.TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators;
            ParameterListTransformers = settings.Request.ParameterListTransformers.ToList();
            QueryParameterListStrategy = settings.Request.QueryParameterListStrategy;
        }

        public ApiInvocation ApiInvocation { get; private set; }
        public string UrlTemplate { get; internal set; }
        public IContentSerializer ContentSerializer { get; set; }
        public Func<object, Parameter, string> ParameterValueToString { get; set; }
        public bool InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl { get; set; }
        public bool TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators { get; set; }
        public List<IParameterListTransformer> ParameterListTransformers { get; set; }
        public IQueryParamaterListStrategy QueryParameterListStrategy { get; set; }
    }
}