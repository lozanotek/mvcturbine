namespace MvcTurbine.Tests.Blades {
    using System;
    using NUnit.Framework;
    using Rhino.Mocks;

    [TestFixture]
    public class BladeTest {
        [Test]
        public void Blade_Spins_When_Spin_Is_Called() {
            var blade = new MockBlade();
            var context = MockRepository.GenerateMock<IRotorContext>();

            blade.Spin(context);

            Assert.IsTrue(blade.HasSpunned);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Blade_Throws_Exception_When_Initialized_Is_Called_With_Null_RotorContext() {
            var blade = new MockBlade();

            blade.Spin(null);
        }

        [Test]
        public void Can_Create_Valid_Instance() {
            var blade = new MockBlade();
            Assert.IsNotNull(blade);
        }

        [Test]
        public void Blade_Is_Initialized_When_Initialized_Is_Called() {
            var blade = new MockBlade();
            blade.Initialize(null);

            Assert.IsTrue(blade.IsInitialized);
        }

        [Test]
        public void Blade_Is_Disposed_When_No_Longer_Used() {
            var blade = new MockBlade();
            blade.Dispose();

            Assert.IsTrue(blade.IsDisposed);
        }
    }
}