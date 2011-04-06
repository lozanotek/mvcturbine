namespace MvcTurbine.Tests.ComponentModel {
    using MvcTurbine.ComponentModel;
    using NUnit.Framework;

    [TestFixture]
    public class ServiceRegistrationTests {
        [Test]
        public void Can_Create_Valid_Instance() {
            var instance = new ServiceRegistration();
            Assert.IsNotNull(instance);
        }

        [Test]
        public void New_Instance_Is_Invalid() {
            var instance = new ServiceRegistration();
            Assert.IsFalse(instance.IsValid());
        }

        [Test]
        public void Registration_With_ServiceType_Only_Is_Invalid() {
            var instance = new ServiceRegistration { ServiceType = typeof(object) };

            Assert.IsFalse(instance.IsValid());
        }

        [Test]
        public void Registration_With_RegistrationHandler_Only_Is_Invalid() {
            var instance = new ServiceRegistration
            {
                RegistrationHandler = (locator, type) => { }
            };

            Assert.IsFalse(instance.IsValid());
        }

        [Test]
        public void Registration_With_TypeFilter_Only_Is_Invalid() {
            var instance = new ServiceRegistration { TypeFilter = (service, reg) => false };

            Assert.IsFalse(instance.IsValid());
        }

        [Test]
        public void Registration_With_All_Properties_Is_Valid() {
            var instance = new ServiceRegistration
            {
                ServiceType = typeof(object),
                RegistrationHandler = (locator, type) => { },
                TypeFilter = (service, reg) => false
            };

            Assert.IsTrue(instance.IsValid());
        }
    }
}