namespace WebAnchor.Tests.RequestFactory.Url
{
    public class UrlEncodeSlashesSettings : DefaultApiSettings
    {
        public UrlEncodeSlashesSettings()
        {
            Request.TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators = false;
        }
    }
}