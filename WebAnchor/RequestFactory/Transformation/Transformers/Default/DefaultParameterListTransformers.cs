using System.Collections.Generic;
using WebAnchor.RequestFactory.Transformation.Transformers.List;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Default
{
    public class DefaultParameterListTransformers : List<IParameterListTransformer>
    {
        public DefaultParameterListTransformers()
        {
            Add(new ParameterCreatorTransformer());
            Add(new ParameterOfListTransformer());
            Add(new AttributesTransformer());
        }
    }
}