namespace MvcTurbine.Web.Tests.Blades {
    using System;
    using System.Web.Mvc;
    using Models;
    using NUnit.Framework;
    using Rhino.Mocks;
    using Web.Blades;

    public class MvcBlade_SetupModelBinderTests : TestFixtureBase {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null_Rotor_Context_Should_Throw_ArgumentNullException() {
            var blade = new MvcBlade();
            blade.SetupModelBinders(null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Null_Service_Locator_Should_Throw_InvalidOperationException() {
            var context = Get<IRotorContext>();

            using (Record()) {
                Expect.Call(context.ServiceLocator).Return(null);
            }

            using (Playback()) {
                var blade = new MvcBlade();
                blade.SetupModelBinders(context);
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Valid_Service_Locator_Should_Set_TurbineModelBinder() {
            var context = Get<IRotorContext>();

            using (Record()) {
                Expect.Call(context.ServiceLocator).Return(new MockControllerFactoryServiceLocator());
            }

            using (Playback()) {
                var blade = new MvcBlade();
                blade.SetupModelBinders(context);
            }

            var binder = ModelBinders.Binders.DefaultBinder;
            Assert.IsNotNull(binder);
            Assert.AreEqual(binder.GetType(), typeof(TurbineModelBinder));
        }
    }
}
