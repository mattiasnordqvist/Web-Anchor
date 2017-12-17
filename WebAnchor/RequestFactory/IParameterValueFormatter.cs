namespace WebAnchor.RequestFactory
{
    public interface IParameterValueFormatter
    {
        string Format(object value, Parameter parameter);
    }
}