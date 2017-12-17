using System;
using System.Collections.Generic;
using System.Globalization;
using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers.Default;

namespace WebAnchor
{
    public class DefaultApiRequestSettings : IApiRequestSettings
    {
        public DefaultApiRequestSettings()
        {
            ParameterListTransformers = new DefaultParameterListTransformers();
            InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl = true;
            TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators = true;
            ContentSerializer = new JsonContentSerializer(new Newtonsoft.Json.JsonSerializer());
            ParameterToString = parameter =>
            {
                var value = (parameter.Value ?? parameter.SourceValue);
                return value is IFormattable ? ((IFormattable)value).ToString(null, CultureInfo.InvariantCulture) : value.ToString();
            };
        }

        public virtual List<IParameterListTransformer> ParameterListTransformers { get; set; }
        public virtual bool InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl { get; set; }

        public virtual bool TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators { get; set; }
        public virtual IContentSerializer ContentSerializer { get; set; }
        public Func<Parameter, string> ParameterToString { get; set; }
    }
}