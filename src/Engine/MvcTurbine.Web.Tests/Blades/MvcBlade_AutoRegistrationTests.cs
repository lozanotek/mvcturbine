namespace MvcTurbine.Web.Tests.Blades {
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using ComponentModel;
    using NUnit.Framework;
    using Web.Blades;

    [TestFixture]
    public class MvcBlade_AutoRegistrationTests {
        [Test]
        public void Default_MvcBlade_Returns_Filled_Registration_List() {
            var blade = new MvcBlade();

            var list = new AutoRegistrationList();
            blade.AddRegistrations(list);

            Assert.IsNotEmpty(list.ToList());
            Assert.AreEqual(list.Count(), 7);
        }

        [Test]
        public void Default_MvcBlade_Returns_Filled_Registration_List_With_MVC_Registrations() {
            var blade = new MvcBlade();

            var list = new AutoRegistrationList();
            blade.AddRegistrations(list);

            foreach (var registration in list) {
                Assert.IsTrue(registration.IsValid());
                Assert.IsTrue(registration.ServiceType.IsMvcType());
            }
        }
    }

    static class TypeExtesion {
        public static bool IsMvcType(this Type type) {
            return type.IsType<IController>() ||
                    type.IsType<IViewEngine>() ||
                    type.IsType<IModelBinder>() ||
                    type.IsType<IAuthorizationFilter>() ||
                    type.IsType<IActionFilter>() ||
                    type.IsType<IResultFilter>() ||
                    type.IsType<IExceptionFilter>();
        }
    }
}