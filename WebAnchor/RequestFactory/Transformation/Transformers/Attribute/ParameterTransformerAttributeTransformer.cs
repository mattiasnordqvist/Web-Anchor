namespace WebAnchor.RequestFactory.Transformation.Transformers.Attribute
{
    public class ParameterTransformerAttributeTransformer : ParameterTransformer<ParameterTransformerAttribute>
    {
        protected override void Transform(Parameter parameter, ParameterTransformerAttribute attribute)
        {
            attribute.Context = Context;
            attribute.Apply(parameter);
        }
    }
}