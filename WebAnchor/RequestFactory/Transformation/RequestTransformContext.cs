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
            ParameterToString = settings.Request.ParameterToString;
            InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl = settings.Request.InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl;
            TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators = settings.Request.TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators;
            ParameterListTransformers = settings.Request.ParameterListTransformers.ToList();
        }

        public ApiInvocation ApiInvocation { get; private set; }
        public string UrlTemplate { get; internal set; }
        public IContentSerializer ContentSerializer { get; set; }
        public Func<Parameter, string> ParameterToString { get; set; }
        public bool InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl { get; set; }
        public bool TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators { get; set; }
        public List<IParameterListTransformer> ParameterListTransformers { get; set; }
    }
}