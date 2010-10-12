namespace MvcTurbine.Tests.ComponentModel {
    using System;
    using MvcTurbine.ComponentModel;
    using NUnit.Framework;

    [TestFixture]
    public class RegistrationTests {
        [Test]
        public void Simple_Registration_Returns_Valid_Service_Registration() {
            ServiceRegistration result = Registration.Simple<IService>();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid());

            Assert.AreEqual(result.ServiceType, typeof(IService));
        }

        [Test]
        public void Simple_Registration_With_Filter_Returns_Valid_Service_Registration() {
            ServiceRegistration result = Registration.Simple<IService>(RegistrationFilters.DefaultFilter);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid());

            Assert.AreEqual(result.ServiceType, typeof(IService));
        }

        [Test]
        public void Keyed_Registration_Returns_Valid_Service_Registration() {
            ServiceRegistration result = Registration.Keyed<IService>();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid());

            Assert.AreEqual(result.ServiceType, typeof(IService));
        }

        [Test]
        public void Keyed_Registration_With_Filter_Returns_Valid_Service_Registration() {
            ServiceRegistration result = Registration.Keyed<IService>(RegistrationFilters.DefaultFilter);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid());

            Assert.AreEqual(result.ServiceType, typeof(IService));
        }

        [Test]
        public void Custom_Registration_With_Nulls_Returns_Invalid_Service_Registration() {
            ServiceRegistration result = Registration.Custom<IService>(null, null);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid());

            Assert.AreEqual(result.ServiceType, typeof(IService));
        }

        [Test]
        public void Custom_Registration_With_Valids_Returns_Invalid_Service_Registration() {
            var result = Registration.Custom<IService>(RegistrationFilters.DefaultFilter,
                TestActions.SimpleRegistration<IService>());

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid());

            Assert.AreEqual(result.ServiceType, typeof(IService));
        }
    }

    internal static class TestActions {
        public static Action<IServiceLocator, Type> SimpleRegistration<TService>() where TService : class {
            return (locator, type) => locator.Register<TService>(type);
        }
    }

    internal interface IService {
    }
}