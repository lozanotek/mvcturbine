namespace MvcTurbine.Tests {
    using NUnit.Framework;

    [TestFixture]
    public class TypeExtensionsTests {
        [Test]
        public void String_Type_Should_Be_Object_Type() {
            bool result = typeof (string).IsType<object>();
            Assert.IsTrue(result);
        }

        [Test]
        public void String_Type_Should_Be_String_Type() {
            bool result = typeof (string).IsType<string>();
            Assert.IsTrue(result);
        }

        [Test]
        public void String_Type_Should_Not_Be_Integer_Type() {
            bool result = typeof (string).IsType<int>();
            Assert.IsFalse(result);
        }

        [Test]
        public void IBar_Type_Should_Be_IFoo_Type() {
            bool result = typeof(IBar).IsType<IFoo>();
            Assert.IsTrue(result);
        }

        [Test]
        public void IBar_Type_Should_Not_Be_String_Type() {
            bool result = typeof(IBar).IsType<string>();
            Assert.IsFalse(result);
        }
    }

    internal interface IFoo {
    }

    internal interface IBar : IFoo {
    }
}
