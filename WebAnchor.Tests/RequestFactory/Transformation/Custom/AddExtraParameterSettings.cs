namespace WebAnchor.Tests.RequestFactory.Transformation.Custom
{
    public class AddExtraParameterSettings : DefaultApiSettings
    {
        public AddExtraParameterSettings()
        {
            Request.ParameterListTransformers.Add(new AddExtraParameterTransformer("extra", 3));
        }
    }
}