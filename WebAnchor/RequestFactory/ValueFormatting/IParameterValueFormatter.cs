namespace WebAnchor.RequestFactory.ValueFormatting
{
    public interface IParameterValueFormatter
    {
        string Format(object value, Parameter parameter);
    }
}