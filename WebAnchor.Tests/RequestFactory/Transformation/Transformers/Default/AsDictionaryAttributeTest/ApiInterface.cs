using WebAnchor.RequestFactory.HttpAttributes;
using WebAnchor.RequestFactory.Transformation.Transformers;
using WebAnchor.RequestFactory.Transformation.Transformers.Default;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Default.AsDictionaryAttributeTest
{
    [AsDictionary]
    public interface IApiInterfaceWithAttribute
    {
        [Post("")]
        void NoAttribute([Content] object unknown);
    }

    public interface IApiInterface
    {
        [Post("")]
        void NoAttribute([Content] object unknown);

        [Post("")]
        void AttributeOnParameter([Content][AsDictionary] object unknown);

        [Post("")]
        [AsDictionary]
        void AttributeOnMethod([Content] object unknown);
    }
}
