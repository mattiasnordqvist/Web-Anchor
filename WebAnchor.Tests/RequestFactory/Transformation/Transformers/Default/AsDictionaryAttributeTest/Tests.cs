using System.Collections.Generic;
using System.Linq;

using WebAnchor.Tests.TestUtils;

using Xunit;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Default.AsDictionaryAttributeTest
{
    public class Tests : WebAnchorTest
    {
        [Fact]
        public void NoAttribute()
        {
            TestTheRequest<IApiInterface>(
                x => x.NoAttribute(new AClass { A = "hej" }),
                null,
                null,
                (a, b) => Assert.Equal(typeof(AClass), a.First().Value.GetType()));
        }

        [Fact]
        public void AttributeOnlyOnContentType()
        {
            TestTheRequest<IApiInterface>(
                x => x.NoAttribute(new AClassAsDictionary { A = "hej" }),
                null,
                null,
                (a, b) => Assert.True(a.First().Value is IDictionary<string, object>));
        }

        [Fact]
        public void AttributeOnlyOnParameter()
        {
            TestTheRequest<IApiInterface>(
                x => x.AttributeOnParameter(new AClass { A = "hej" }),
                null,
                null,
                (a, b) => Assert.True(a.First().Value is IDictionary<string, object>));
        }

        [Fact]
        public void AttributeOnlyOnMethod()
        {
            TestTheRequest<IApiInterface>(
               x => x.AttributeOnParameter(new AClass { A = "hej" }),
               null,
               null,
               (a, b) => Assert.True(a.First().Value is IDictionary<string, object>));
        }

        [Fact]
        public void AttributeOnlyOnApi()
        {
            TestTheRequest<IApiInterfaceWithAttribute>(
               x => x.NoAttribute(new AClass { A = "hej" }),
               null,
               null,
               (a, b) => Assert.True(a.First().Value is IDictionary<string, object>));
        }
    }
}
