namespace WebAnchor.Tests.RequestFactory.Transformation.Custom
{
    public class AddExtraParameterSettings : DefaultApiSettings
    {
        public AddExtraParameterSettings()
        {
            Request.ParameterListTransformers.Add(new AddExtraQueryParameterTransformer("extra", new object[] { 3 }));
        }
    }
}