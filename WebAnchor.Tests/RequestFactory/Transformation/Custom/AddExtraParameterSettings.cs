namespace WebAnchor.Tests.RequestFactory.Transformation.Custom
{
    public class AddExtraParameterSettings : ApiSettings
    {
        public AddExtraParameterSettings()
        {
            ParameterListTransformers.Add(new AddExtraParameterTransformer("extra", 3));
        }
    }
}