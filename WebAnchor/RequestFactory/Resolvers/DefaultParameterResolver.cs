namespace WebAnchor.RequestFactory.Resolvers
{
    public class DefaultParameterResolver : IParameterResolver
    {
        public bool CanResolve(Parameter parameter)
        {
            return true;
        }

        public void Resolve(Parameter parameter)
        {
            if (parameter.ParameterInfo != null)
            {
                parameter.Name = parameter.ParameterInfo.Name;    
            }
            
            parameter.Value = parameter.ParameterValue == null ? null : parameter.ParameterValue.ToString();
        }
    }
}