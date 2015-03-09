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
            var name = parameter.ParameterInfo.Name;
            parameter.Name = name;
            parameter.Value = parameter.ParameterValue == null ? null : parameter.ParameterValue.ToString();
        }
    }
}