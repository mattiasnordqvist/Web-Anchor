using System.Collections.Generic;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Attribute
{
    public abstract class ParameterTransformer<T> : ParameterListTransformerBase
        where T : System.Attribute
    {
        protected ParameterTransformContext Context { get; set; }

        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            Context = parameterTransformContext;
            foreach (var parameter in parameters)
            {
                ResolveParameter(parameter);
            }

            return parameters;
        }

        protected abstract void Transform(Parameter parameter, T attribute);

        private void ResolveParameter(Parameter parameter)
        {
            if (parameter.SourceParameterInfo != null)
            {
                var attributes = parameter.SourceParameterInfo.GetAttributesChain<T>();
                foreach (var attribute in attributes)
                {
                    Transform(parameter, attribute);
                }
            }
        }
    }
}