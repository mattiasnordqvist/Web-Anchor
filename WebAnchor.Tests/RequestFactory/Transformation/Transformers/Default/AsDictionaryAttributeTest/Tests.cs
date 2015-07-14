using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Default.AsDictionaryAttributeTest
{
    [TestFixture]
    public class Tests : WebAnchorTest
    {
        [Test]
        public void NoAttribute()
        {
            TestTheRequest<IApiInterface>(
                x => x.NoAttribute(new AClass { A = "hej" }),
                null,
                null,
                (a, b) => Assert.AreEqual(typeof(AClass), a.First().Value.GetType()));
        }

        [Test]
        public void AttributeOnlyOnContentType()
        {
            TestTheRequest<IApiInterface>(
                x => x.NoAttribute(new AClassAsDictionary { A = "hej" }),
                null,
                null,
                (a, b) => Assert.IsTrue(a.First().Value is IDictionary<string, object>));
        }

        [Test]
        public void AttributeOnlyOnParameter()
        {
            TestTheRequest<IApiInterface>(
                x => x.AttributeOnParameter(new AClass { A = "hej" }),
                null,
                null,
                (a, b) => Assert.IsTrue(a.First().Value is IDictionary<string, object>));
        }

        [Test]
        public void AttributeOnlyOnMethod()
        {
            TestTheRequest<IApiInterface>(
               x => x.AttributeOnParameter(new AClass { A = "hej" }),
               null,
               null,
               (a, b) => Assert.IsTrue(a.First().Value is IDictionary<string, object>));
        }

        [Test]
        public void AttributeOnlyOnApi()
        {
            TestTheRequest<IApiInterfaceWithAttribute>(
               x => x.NoAttribute(new AClass { A = "hej" }),
               null,
               null,
               (a, b) => Assert.IsTrue(a.First().Value is IDictionary<string, object>));
        }
    }
}
