namespace MvcTurbine.Tests.ComponentModel {
    using System;
    using MvcTurbine.ComponentModel;
    using NUnit.Framework;

    [TestFixture]
    public class RegistrationFiltersTests {
        [Test]
        public void DefaultFilter_Returns_False_When_Checked_With_Attribute() {
            Type attributeType = typeof(Attribute);

            bool result = RegistrationFilters.DefaultFilter(typeof(IService), attributeType);
            Assert.IsFalse(result);
        }

        [Test]
        public void DefaultFilter_Returns_False_When_Checked_With_Not_Assignable_Types() {
            Type registrationType = typeof(IOtherService);

            bool result = RegistrationFilters.DefaultFilter(typeof(IService), registrationType);
            Assert.IsFalse(result);
        }

        [Test]
        public void DefaultFilter_Returns_False_When_Checked_With_Same_Types() {
            Type registrationType = typeof(IService);

            bool result = RegistrationFilters.DefaultFilter(typeof(IService), registrationType);
            Assert.IsFalse(result);
        }

        [Test]
        public void DefaultFilter_Returns_False_When_Checked_With_Generic_Type() {
            Type registrationType = typeof(IChildService<>);

            bool result = RegistrationFilters.DefaultFilter(typeof(IService), registrationType);
            Assert.IsFalse(result);
        }

        [Test]
        public void DefaultFilter_Returns_False_When_Checked_With_Abstract_Type() {
            Type registrationType = typeof(AbstractService);

            bool result = RegistrationFilters.DefaultFilter(typeof(IService), registrationType);
            Assert.IsFalse(result);
        }

        [Test]
        public void DefaultFilter_Returns_True_When_Checked_With_Valid_Concrete_Type() {
            Type registrationType = typeof(ConcreteService);

            bool result = RegistrationFilters.DefaultFilter(registrationType, typeof (IService));
            Assert.IsTrue(result);
        }
    }

    internal interface IOtherService {
    }

    internal interface IChildService<T> : IService {
        void Work(T data);
    }

    internal abstract class AbstractService : IService {
    }

    internal class ConcreteService : IService {
    }
}