using System.Collections.Generic;

using NUnit.Framework;

using WebAnchor.RequestFactory.Resolvers;

namespace WebAnchor.Tests
{
    [TestFixture]
    public class ObjectToDictionaryHelperTests
    {
        [Test]
        public void Test0()
        {
            var dict = new Customer { Id = 1, Name = null }.ToDictionary();
            Assert.AreEqual(2, dict.Keys.Count);
            Assert.AreEqual(1, dict["Id"]);
            Assert.AreEqual(null, dict["Name"]);
        }

        [Test]
        public void Test1()
        {
            var dict = new { Id = 1, Name = "Hej" }.ToDictionary();
            Assert.AreEqual(2, dict.Keys.Count);
            Assert.AreEqual(1, dict["Id"]);
            Assert.AreEqual("Hej", dict["Name"]);
        }

        [Test]
        public void Test2()
        {
            var dict = new { Id = 1, Name = "Hej", Child = new { Id = 2, Name = "Hejhej" } }.ToDictionary();
            Assert.AreEqual(3, dict.Keys.Count);
            Assert.AreEqual(1, dict["Id"]);
            Assert.AreEqual("Hej", dict["Name"]);
            Assert.IsTrue(dict["Child"] is IDictionary<string, object>);
            Assert.AreEqual(2, ((IDictionary<string, object>)dict["Child"])["Id"]);
            Assert.AreEqual("Hejhej", ((IDictionary<string, object>)dict["Child"])["Name"]);
        }

        [Test]
        public void Test3()
        {
            var dict = new Dictionary<string, object> { { "Id", 1 }, { "Name", "Hej" } }.ToDictionary();
            Assert.AreEqual(2, dict.Keys.Count);
            Assert.AreEqual(1, dict["Id"]);
            Assert.AreEqual("Hej", dict["Name"]);
        }

        [Test]
        public void Test4()
        {
            var dict = new Dictionary<string, object> { { "Id", 1 }, { "Name", "Hej" }, { "Child", new Dictionary<string, object> { { "Id", 2 }, { "Name", "Hejhej" } } } }.ToDictionary();
            Assert.AreEqual(3, dict.Keys.Count);
            Assert.AreEqual(1, dict["Id"]);
            Assert.AreEqual("Hej", dict["Name"]);
            Assert.IsTrue(dict["Child"] is IDictionary<string, object>);
            Assert.AreEqual(2, ((IDictionary<string, object>)dict["Child"])["Id"]);
            Assert.AreEqual("Hejhej", ((IDictionary<string, object>)dict["Child"])["Name"]);
        }

        [Test]
        public void Test5()
        {
            var dict =
                new Dictionary<string, object> { { "Id", 1 }, { "Names", new List<string>() { "Hej", "Då" } } }
                    .ToDictionary();
            Assert.AreEqual(2, dict.Keys.Count);
            Assert.AreEqual(1, dict["Id"]);
            
            Assert.IsTrue(dict["Names"] is IList<object>);
            Assert.AreEqual(2, ((IList<object>)dict["Names"]).Count);
            Assert.AreEqual("Hej", ((IList<object>)dict["Names"])[0]);
            Assert.AreEqual("Då", ((IList<object>)dict["Names"])[1]);
        }

        [Test]
        public void Test6()
        {
            var dict = new Dictionary<string, object> { { "Id", 1 }, { "Names", new List<object>() { new { Id = 1, Name = "Hej" }, new { Id = 2, Name = "Hejhej" } } } }.ToDictionary();
            Assert.AreEqual(2, dict.Keys.Count);
            Assert.AreEqual(1, dict["Id"]);

            Assert.IsTrue(dict["Names"] is IList<object>);
            Assert.AreEqual(2, ((IList<object>)dict["Names"]).Count);
            Assert.AreEqual(1, ((IDictionary<string, object>)((IList<object>)dict["Names"])[0])["Id"]);
            Assert.AreEqual("Hej", ((IDictionary<string, object>)((IList<object>)dict["Names"])[0])["Name"]);
            Assert.AreEqual(2, ((IDictionary<string, object>)((IList<object>)dict["Names"])[1])["Id"]);
            Assert.AreEqual("Hejhej", ((IDictionary<string, object>)((IList<object>)dict["Names"])[1])["Name"]);
        }
    }
}
