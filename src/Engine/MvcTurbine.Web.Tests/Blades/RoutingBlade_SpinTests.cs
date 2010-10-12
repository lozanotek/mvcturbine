namespace MvcTurbine.Web.Tests.Blades {
    using System;
    using System.Web.Mvc;
    using Controllers;
    using NUnit.Framework;
    using Rhino.Mocks;
    using Web.Blades;
    using Web.Controllers;
    using Moq;

    [TestFixture]
    public class RoutingBlade_SpinTests : TestFixtureBase {
        [Test]
        public void Resolve_Route_Registrators_Returns_DefaultControllerFactory() {
            var contextFake = new Mock<IRotorContext>();
            var locator = new MockControllerFactoryServiceLocator();

            contextFake.Setup(x => x.ServiceLocator)
                .Returns(locator);

            var blade = new MvcBlade();
            blade.SetupControllerFactory(contextFake.Object);

            var currentFactory = ControllerBuilder.Current.GetControllerFactory();
            Assert.IsNotNull(currentFactory);
        }

        [Test]
        public void Resolve_Controller_Factory_Returns_TurbineControllerFactory() {
            
            var locator = new MockControllerFactoryServiceLocator()
            {
                ShouldThrowExceptionForControllerFactory = true
            };

            var contextFake = new Mock<IRotorContext>();
            contextFake.Setup(x => x.ServiceLocator)
                .Returns(locator);

            var blade = new MvcBlade();
            blade.SetupControllerFactory(contextFake.Object);

            IControllerFactory currentFactory = ControllerBuilder.Current.GetControllerFactory();

            Assert.IsNotNull(currentFactory);
            Assert.AreEqual(currentFactory.GetType(), typeof(TurbineControllerFactory));
        }

        [Test]
        public void Resolve_Controller_Factory_Returns_Null_Which_Implies_TurbineControllerFactory() {
            
            var locator = new MockControllerFactoryServiceLocator()
            {
                ShouldReturnNullForControllerFactory = true
            };

            var contextFake = new Mock<IRotorContext>();
            contextFake.Setup(x => x.ServiceLocator)
                .Returns(locator);

            var blade = new MvcBlade();
            blade.SetupControllerFactory(contextFake.Object);

            var currentFactory = ControllerBuilder.Current.GetControllerFactory();
            Assert.IsNotNull(currentFactory);
            Assert.AreEqual(currentFactory.GetType(), typeof(TurbineControllerFactory));
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Null_Rotor_Context_Should_Throw_ArgumentNullException() {
            var blade = new RoutingBlade();
            blade.Spin(null);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Null_Service_Locator_Should_Throw_InvalidOperationException() {
            var context = Get<IRotorContext>();

            using (Record()) {
                Expect.Call(context.ServiceLocator).Return(null);
            }

            using (Playback()) {
                var blade = new RoutingBlade();
                blade.Spin(context);
            }
        }
    }
}