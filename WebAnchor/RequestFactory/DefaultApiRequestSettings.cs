using System;
using System.Collections.Generic;
using System.Globalization;
using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers.Default;

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
            ParameterValueToString = (value, originalParameter) =>
            {
                return value is IFormattable ? ((IFormattable)value).ToString(null, CultureInfo.InvariantCulture) : value.ToString();
            };
            QueryParameterListStrategy = new NormalQueryParamaterListStrategy();
        }

        public virtual List<IParameterListTransformer> ParameterListTransformers { get; set; }
        public virtual bool InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl { get; set; }

        public virtual bool TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators { get; set; }
        public virtual IContentSerializer ContentSerializer { get; set; }
        public virtual Func<object, Parameter, string> ParameterValueToString { get; set; }
        public virtual IQueryParamaterListStrategy QueryParameterListStrategy { get; set; }
    }
}