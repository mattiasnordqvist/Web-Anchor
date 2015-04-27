namespace WebAnchor.RequestFactory.Resolvers
{
    public class DefaultParameterResolver : IParameterResolver
    {
        public void Resolve(Parameter parameter)
        {
            if (parameter.ParameterInfo != null)
            {
                parameter.Name = parameter.ParameterInfo.Name;    
            }

            if (parameter.ParameterType == ParameterType.Content)
            {
                parameter.Value = parameter.ParameterValue.ToDictionary();
            }
            else
            {
                parameter.Value = parameter.ParameterValue == null ? null : parameter.ParameterValue.ToString();
            }
        }
    }
}