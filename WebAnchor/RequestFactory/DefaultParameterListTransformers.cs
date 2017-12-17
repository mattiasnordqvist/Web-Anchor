using System.Collections.Generic;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers.Default;

namespace WebAnchor.RequestFactory
{
    public class DefaultParameterListTransformers : List<IParameterListTransformer>
    {
        public DefaultParameterListTransformers()
        {
            Add(new ParameterCreatorTransformer());
            Add(new AttributesTransformer());
        }
    }
}