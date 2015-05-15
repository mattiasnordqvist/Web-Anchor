using WebAnchor.RequestFactory.Transformation.Transformers.Attribute;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Alias
{
    public class AliasTransformer : ParameterTransformer<AliasAttribute>
    {
        protected override void Transform(Parameter parameter, AliasAttribute attribute)
        {
            parameter.Name = attribute.Alias;
        }
    }
}