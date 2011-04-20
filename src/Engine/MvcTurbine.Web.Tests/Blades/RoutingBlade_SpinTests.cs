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