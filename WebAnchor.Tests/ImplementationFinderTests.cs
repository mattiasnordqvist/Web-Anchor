using Xunit;

namespace WebAnchor.ApiGenerator.Tests
{
    public class ImplementationFinderTests
    {
        [Fact]
        public void MatchExists()
        {
            
            var sut = new ImplementationFinder();
            var match = sut.FindMatch(new System.Type[] { typeof(Class1), typeof(Class2), typeof(Class3<>) }, typeof(Interface1));
            Assert.Equal(typeof(Class1), match);
        }

        [Fact]
        public void GenericMatching()
        {
            var sut = new ImplementationFinder();
            var match = sut.FindMatch(new System.Type[] { typeof(Class3<object>), typeof(Class1), typeof(Class2), typeof(Class3<>) }, typeof(Interface3<object>));
            Assert.Equal(typeof(Class3<object>), match);
        }

        [Fact]
        public void GenericCreationMatching()
        {
            var sut = new ImplementationFinder();
            var match = sut.FindMatch(new System.Type[] { typeof(Class1), typeof(Class2), typeof(Class3<>), typeof(Class4<>) }, typeof(Interface3<object>));
            Assert.Equal(typeof(Class3<object>), match);
        }
    }

    public interface Interface1 { }
    public interface Interface2 { }
    public interface Interface3<T> { }

    public class Class1 : Interface1 { }
    public class Class2 : Interface2 { }
    public class Class3<T> : Interface3<T> { }

    public class Class4<T> : Interface3<T> where T : Test{ }
    public class Test {}
}