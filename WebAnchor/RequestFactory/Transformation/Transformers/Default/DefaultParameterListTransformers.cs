using System.Collections.Generic;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute.List;
using WebAnchor.RequestFactory.Transformation.Transformers.Formattable;
using WebAnchor.RequestFactory.Transformation.Transformers.List;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Default
{
    public class DefaultParameterListTransformers : List<IParameterListTransformer>
    {
        public DefaultParameterListTransformers()
        {
            Add(new ParameterCreatorTransformer());
            Add(new ParameterOfListTransformer());
            Add(new DefaultParameterTransformer());
            Add(new FormattableParameterResolver());
            Add(new ParameterListTransformerAttributeTransformer());
            Add(new ParameterTransformerAttributeTransformer());
        }
    }
}