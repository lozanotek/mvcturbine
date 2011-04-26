namespace MvcTurbine.Web.Tests.Blades {
    using MvcTurbine.Blades;
    using NUnit.Framework;
    using Web.Blades;

    [TestFixture]
    public class BladeExtensionsTests {
        [Test]
        public void Mock_Blade_Is_Not_Core_Blade() {
            bool result = new MockBlade().IsCoreBlade();
            Assert.IsFalse(result);
        }

        [Test]
        public void Routing_Blade_Is_Not_Core_Blade() {
            bool result = new RoutingBlade().IsCoreBlade();
            Assert.IsFalse(result);
        }
    }

    internal class MockBlade : IBlade {
        #region IBlade Members

        public void Dispose() {
        }

        public void Initialize(IRotorContext context) {
        }

        public void Spin(IRotorContext context) {
        }

        #endregion
    }
}