using System;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Default
{
    [Obsolete("The ContentAsDictionaryAttribute will be removed in a future version. You can implement it yourself if you really need it, but maybe you can modify your content object before you send it to webanchor?")]
    public class ContentAsDictionaryAttribute : System.Attribute
    {
    }
}