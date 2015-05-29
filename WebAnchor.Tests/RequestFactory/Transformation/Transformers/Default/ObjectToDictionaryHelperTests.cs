using System.Collections.Generic;

using NUnit.Framework;

using WebAnchor.RequestFactory.Transformation.Transformers.Default;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Default
{
    [TestFixture]
    public class ObjectToDictionaryHelperTests
    {
        [Test]
        public void FlatObjectWithNullValues()
        {
            var dictionary = new Fixtures.Entity { Id = 1, Name = null }.ToDictionary();
            
            Assert.AreEqual(2, dictionary.Keys.Count);
            Assert.AreEqual(1, dictionary["Id"]);
            Assert.AreEqual(null, dictionary["Name"]);
        }

        [Test]
        public void FlatObjectWithoutNullValues()
        {
            var dictionary = new { Id = 1, Name = "Hej" }.ToDictionary();
            
            Assert.AreEqual(2, dictionary.Keys.Count);
            Assert.AreEqual(1, dictionary["Id"]);
            Assert.AreEqual("Hej", dictionary["Name"]);
        }

        [Test]
        public void AnonymousTypeWithNestedAnonymousType()
        {
            var dictionary = new { Id = 1, Name = "Hej", Child = new { Id = 2, Name = "Hejhej" } }.ToDictionary();
            
            Assert.AreEqual(3, dictionary.Keys.Count);
            Assert.AreEqual(1, dictionary["Id"]);
            Assert.AreEqual("Hej", dictionary["Name"]);
            Assert.IsTrue(dictionary["Child"] is IDictionary<string, object>);
            Assert.AreEqual(2, ((IDictionary<string, object>)dictionary["Child"])["Id"]);
            Assert.AreEqual("Hejhej", ((IDictionary<string, object>)dictionary["Child"])["Name"]);
        }

        [Test]
        public void StringObjectDictionary()
        {
            var dictionary = new Dictionary<string, object> { { "Id", 1 }, { "Name", "Hej" } }.ToDictionary();
            
            Assert.AreEqual(2, dictionary.Keys.Count);
            Assert.AreEqual(1, dictionary["Id"]);
            Assert.AreEqual("Hej", dictionary["Name"]);
        }

        [Test]
        public void StringObjectDictionaryWithNestedStringObjectDictionary()
        {
            var dictionary = new Dictionary<string, object> { { "Id", 1 }, { "Name", "Hej" }, { "Child", new Dictionary<string, object> { { "Id", 2 }, { "Name", "Hejhej" } } } }.ToDictionary();
            
            Assert.AreEqual(3, dictionary.Keys.Count);
            Assert.AreEqual(1, dictionary["Id"]);
            Assert.AreEqual("Hej", dictionary["Name"]);
            Assert.IsTrue(dictionary["Child"] is IDictionary<string, object>);
            Assert.AreEqual(2, ((IDictionary<string, object>)dictionary["Child"])["Id"]);
            Assert.AreEqual("Hejhej", ((IDictionary<string, object>)dictionary["Child"])["Name"]);
        }

        [Test]
        public void StringObjectDictionaryWithNestedStringList()
        {
            var dictionary = new Dictionary<string, object> { { "Id", 1 }, { "Names", new List<string> { "Hej", "Då" } } }.ToDictionary();
            
            Assert.AreEqual(2, dictionary.Keys.Count);
            Assert.AreEqual(1, dictionary["Id"]);
            Assert.IsTrue(dictionary["Names"] is IList<object>);
            Assert.AreEqual(2, ((IList<object>)dictionary["Names"]).Count);
            Assert.AreEqual("Hej", ((IList<object>)dictionary["Names"])[0]);
            Assert.AreEqual("Då", ((IList<object>)dictionary["Names"])[1]);
        }

        [Test]
        public void StringObjectDictionaryWithNestedListOfAnonymousObjects()
        {
            var dictionary = new Dictionary<string, object> { { "Id", 1 }, { "Names", new List<object> { new { Id = 1, Name = "Hej" }, new { Id = 2, Name = "Hejhej" } } } }.ToDictionary();
            
            Assert.AreEqual(2, dictionary.Keys.Count);
            Assert.AreEqual(1, dictionary["Id"]);
            Assert.IsTrue(dictionary["Names"] is IList<object>);
            Assert.AreEqual(2, ((IList<object>)dictionary["Names"]).Count);
            Assert.AreEqual(1, ((IDictionary<string, object>)((IList<object>)dictionary["Names"])[0])["Id"]);
            Assert.AreEqual("Hej", ((IDictionary<string, object>)((IList<object>)dictionary["Names"])[0])["Name"]);
            Assert.AreEqual(2, ((IDictionary<string, object>)((IList<object>)dictionary["Names"])[1])["Id"]);
            Assert.AreEqual("Hejhej", ((IDictionary<string, object>)((IList<object>)dictionary["Names"])[1])["Name"]);
        }
    }
}
