namespace MvcTurbine.Tests.ComponentModel {
    using System.Linq;
    using MvcTurbine.ComponentModel;
    using NUnit.Framework;

    [TestFixture]
    public class AutoRegistrationListTest {
        [Test]
        public void Can_Add_Null_Registration_To_List() {
            var list = new AutoRegistrationList();
            list.Add(null).Add(null);

            Assert.IsNotEmpty(list.ToList());
            Assert.AreEqual(list.Count(), 2);
        }

        [Test]
        public void Can_Add_Null_Registration_To_List_And_Clear() {
            var list = new AutoRegistrationList();
            list.Add(null).Add(null);

            Assert.IsNotEmpty(list.ToList());
            Assert.AreEqual(list.Count(), 2);

            list.Clear();

            Assert.IsEmpty(list.ToList());
        }

        [Test]
        public void Can_Create_Valid_Instance() {
            var list = new AutoRegistrationList();
            Assert.IsNotNull(list);
        }

        [Test]
        public void Can_Add_Valid_Registration_To_List() {
            var registration = new ServiceRegistration();
            var list = new AutoRegistrationList {registration, registration};

            Assert.IsNotEmpty(list.ToList());
            Assert.AreEqual(list.Count(), 2);
        }

        [Test]
        public void Can_Add_Valid_Registration_To_List_And_Clear() {
            var registration = new ServiceRegistration();
            var list = new AutoRegistrationList { registration, registration };

            Assert.IsNotEmpty(list.ToList());
            Assert.AreEqual(list.Count(), 2);

            list.Clear();

            Assert.IsEmpty(list.ToList());
        }
    }
}