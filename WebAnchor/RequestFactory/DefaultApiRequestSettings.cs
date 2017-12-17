using System;
using System.Collections.Generic;
using System.Globalization;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.RequestFactory
{
    public class DefaultApiRequestSettings : IApiRequestSettings
    {
        public DefaultApiRequestSettings()
        {
            ParameterListTransformers = new DefaultParameterListTransformers();
            InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl = true;
            TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators = true;
            ContentSerializer = new JsonContentSerializer(new Newtonsoft.Json.JsonSerializer());
            ParameterValueFormatter = new DefaultParameterValueFormatter();
            QueryParameterListStrategy = new NormalQueryParamaterListStrategy();
        }

        public virtual List<IParameterListTransformer> ParameterListTransformers { get; set; }
        public virtual bool InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl { get; set; }

        public virtual bool TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators { get; set; }
        public virtual IContentSerializer ContentSerializer { get; set; }
        public virtual IParameterValueFormatter ParameterValueFormatter { get; set; }
        public virtual IQueryParamaterListStrategy QueryParameterListStrategy { get; set; }
    }
}