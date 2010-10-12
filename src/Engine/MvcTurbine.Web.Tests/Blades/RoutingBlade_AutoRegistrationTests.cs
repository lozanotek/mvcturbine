namespace MvcTurbine.Web.Tests.Blades {
    using System.Linq;
    using ComponentModel;
    using NUnit.Framework;
    using Web.Blades;

    [TestFixture]
    public class RoutingBlade_AutoRegistrationTests {
        [Test]
        public void Default_MvcBlade_Returns_Filled_Registration_List() {
            var blade = new RoutingBlade();

            var list = new AutoRegistrationList();
            blade.AddRegistrations(list);

            Assert.IsNotEmpty(list.ToList());
            Assert.AreEqual(list.Count(), 1);
        }

        [Test]
        public void Default_MvcBlade_Returns_Filled_Registration_List_With_MVC_Registrations() {
            var blade = new RoutingBlade();

            var list = new AutoRegistrationList();
            blade.AddRegistrations(list);

            foreach (ServiceRegistration registration in list) {
                Assert.IsTrue(registration.IsValid());
            }
        }
    }
}