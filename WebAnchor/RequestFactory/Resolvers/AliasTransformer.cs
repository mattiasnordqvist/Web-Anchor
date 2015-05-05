namespace WebAnchor.RequestFactory.Resolvers
{
    public class AliasTransformer : ParameterTransformer<AliasAttribute>
    {
        protected override void Transform(Parameter parameter, AliasAttribute attribute)
        {
            parameter.Name = attribute.Alias;
        }
    }
}