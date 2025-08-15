using System.Collections.Generic;
using System.Linq;
using WebAnchor.RequestFactory.Transformation.Transformers.Default;
using WebAnchor.RequestFactory;
using Xunit;

namespace WebAnchor.Tests.Unit
{
    public class DictionaryParameterTransformerTests
    {
        [Fact]
        public void ValidateApi_DoesNotThrow()
        {
            var transformer = new DictionaryParameterTransformer();
            
            // Should not throw any exceptions
            transformer.ValidateApi(typeof(string));
        }

        [Fact]
        public void Apply_WithNonDictionaryParameters_ReturnsOriginalParameters()
        {
            var transformer = new DictionaryParameterTransformer();
            var regularParameter = Parameter.CreateQueryParameter("test", new[] { "value" });
            var parameters = new List<Parameter> { regularParameter };
            
            var result = transformer.Apply(parameters, null);
            
            Assert.Single(result);
            Assert.Equal("test", result.First().Name);
        }
    }
}