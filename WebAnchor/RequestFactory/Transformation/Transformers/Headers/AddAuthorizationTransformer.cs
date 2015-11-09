namespace WebAnchor.RequestFactory.Transformation.Transformers.Headers
{
    public class AddAuthorizationTransformer : AddHeaderTransformer
    {
        public AddAuthorizationTransformer(string value)
            : base("Authorization", value)
        {
        }
    }
}