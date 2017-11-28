using System.Collections.Generic;
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
            FormatFormattables = true;
            TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators = true;
        }

        public virtual List<IParameterListTransformer> ParameterListTransformers { get; set; }
        public virtual bool InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl { get; set; }
        public virtual bool FormatFormattables { get; set; }
        public virtual bool TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators { get; set; }
        public IDictionary<string, object> CustomSettings { get; set; } = new Dictionary<string, object>();
    }
}