using System.Collections.Generic;

using Xunit;

using WebAnchor.RequestFactory.Transformation.Transformers.Default;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Default.ObjectToDictionary
{
    public class ObjectToDictionaryHelperTests
    {
        [Fact]
        public void FlatObjectWithNullValues()
        {
            var dictionary = new Entity { Id = 1, Name = null }.ToDictionary();
            
            Assert.Equal(2, dictionary.Keys.Count);
            Assert.Equal(1, dictionary["Id"]);
            Assert.Null(dictionary["Name"]);
        }

        [Fact]
        public void FlatObjectWithoutNullValues()
        {
            var dictionary = new { Id = 1, Name = "Hej" }.ToDictionary();
            
            Assert.Equal(2, dictionary.Keys.Count);
            Assert.Equal(1, dictionary["Id"]);
            Assert.Equal("Hej", dictionary["Name"]);
        }

        [Fact]
        public void AnonymousTypeWithNestedAnonymousType()
        {
            var dictionary = new { Id = 1, Name = "Hej", Child = new { Id = 2, Name = "Hejhej" } }.ToDictionary();
            
            Assert.Equal(3, dictionary.Keys.Count);
            Assert.Equal(1, dictionary["Id"]);
            Assert.Equal("Hej", dictionary["Name"]);
            Assert.True(dictionary["Child"] is IDictionary<string, object>);
            Assert.Equal(2, ((IDictionary<string, object>)dictionary["Child"])["Id"]);
            Assert.Equal("Hejhej", ((IDictionary<string, object>)dictionary["Child"])["Name"]);
        }

        [Fact]
        public void StringObjectDictionary()
        {
            var dictionary = new Dictionary<string, object> { { "Id", 1 }, { "Name", "Hej" } }.ToDictionary();
            
            Assert.Equal(2, dictionary.Keys.Count);
            Assert.Equal(1, dictionary["Id"]);
            Assert.Equal("Hej", dictionary["Name"]);
        }

        [Fact]
        public void StringObjectDictionaryWithNestedStringObjectDictionary()
        {
            var dictionary = new Dictionary<string, object> { { "Id", 1 }, { "Name", "Hej" }, { "Child", new Dictionary<string, object> { { "Id", 2 }, { "Name", "Hejhej" } } } }.ToDictionary();
            
            Assert.Equal(3, dictionary.Keys.Count);
            Assert.Equal(1, dictionary["Id"]);
            Assert.Equal("Hej", dictionary["Name"]);
            Assert.True(dictionary["Child"] is IDictionary<string, object>);
            Assert.Equal(2, ((IDictionary<string, object>)dictionary["Child"])["Id"]);
            Assert.Equal("Hejhej", ((IDictionary<string, object>)dictionary["Child"])["Name"]);
        }

        [Fact]
        public void StringObjectDictionaryWithNestedStringList()
        {
            var dictionary = new Dictionary<string, object> { { "Id", 1 }, { "Names", new List<string> { "Hej", "Då" } } }.ToDictionary();
            
            Assert.Equal(2, dictionary.Keys.Count);
            Assert.Equal(1, dictionary["Id"]);
            Assert.True(dictionary["Names"] is IList<object>);
            Assert.Equal(2, ((IList<object>)dictionary["Names"]).Count);
            Assert.Equal("Hej", ((IList<object>)dictionary["Names"])[0]);
            Assert.Equal("Då", ((IList<object>)dictionary["Names"])[1]);
        }

        [Fact]
        public void StringObjectDictionaryWithNestedListOfAnonymousObjects()
        {
            var dictionary = new Dictionary<string, object> { { "Id", 1 }, { "Names", new List<object> { new { Id = 1, Name = "Hej" }, new { Id = 2, Name = "Hejhej" } } } }.ToDictionary();
            
            Assert.Equal(2, dictionary.Keys.Count);
            Assert.Equal(1, dictionary["Id"]);
            Assert.True(dictionary["Names"] is IList<object>);
            Assert.Equal(2, ((IList<object>)dictionary["Names"]).Count);
            Assert.Equal(1, ((IDictionary<string, object>)((IList<object>)dictionary["Names"])[0])["Id"]);
            Assert.Equal("Hej", ((IDictionary<string, object>)((IList<object>)dictionary["Names"])[0])["Name"]);
            Assert.Equal(2, ((IDictionary<string, object>)((IList<object>)dictionary["Names"])[1])["Id"]);
            Assert.Equal("Hejhej", ((IDictionary<string, object>)((IList<object>)dictionary["Names"])[1])["Name"]);
        }
    }
}
