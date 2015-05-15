using System.Collections.Generic;

namespace WebAnchor.RequestFactory.Transformers.Attribute
{
    public abstract class ParameterTransformer<T> : IParameterListTransformer
        where T : System.Attribute
    {
        protected ParameterTransformContext Context { get; set; }

        public virtual IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            this.Context = parameterTransformContext;
            foreach (var parameter in parameters)
            {
                this.ResolveParameter(parameter);
            }

            return parameters;
        }

        protected abstract void Transform(Parameter parameter, T attribute);

        private void ResolveParameter(Parameter parameter)
        {
            if (parameter.ParameterInfo != null)
            {
                var attributes = parameter.ParameterInfo.GetAttributesChain<T>();
                foreach (var attribute in attributes)
                {
                    Transform(parameter, attribute);
                }
            }
        }
    }
}