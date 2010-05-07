using System;
using System.Collections.Generic;
using MvcTurbine.ComponentModel;
using MvcTurbine.FluentValidation.Helpers;
using NUnit.Framework;

namespace MvcTurbine.FluentValidation.Tests.Helpers
{
    [TestFixture]
    public class ServiceLocatorValidatorFactoryTests
    {
        [Test]
        public void Can_resolve_a_validator_after_adding_the_type()
        {
            var serviceLocator = new TestServiceLocator();
            var factory = new ServiceLocatorValidatorFactory(serviceLocator);

            var validatorType = typeof (TestItemValidator);
            var typeToValidate = typeof (TestItem);

            factory.AddValidatorToBeResolved(validatorType);

            factory.GetValidator(typeToValidate);

            Assert.AreEqual(validatorType, serviceLocator.TypeThatWasResolved);
        }

        [Test]
        public void Resolves_the_correct_type_when_multiple_validators_are_added()
        {
            var serviceLocator = new TestServiceLocator();
            var factory = new ServiceLocatorValidatorFactory(serviceLocator);

            var validatorType = typeof (TestItemValidator);
            var typeToValidate = typeof (TestItem);

            factory.AddValidatorToBeResolved(validatorType);
            factory.AddValidatorToBeResolved(typeof (SecondTestItemValidator));

            factory.GetValidator(typeToValidate);

            Assert.AreEqual(validatorType, serviceLocator.TypeThatWasResolved);
        }

        [Test]
        public void Throws_an_exception_if_something_other_than_a_validator_is_added()
        {
            var factory = CreateAServiceLocatorFactory();

            var expectedExceptionWasHit = false;
            try
            {
                factory.AddValidatorToBeResolved(typeof (string));
            }
            catch (ArgumentException argumentException)
            {
                if (argumentException.Message == "May only pass IValidator<T> to AddValidatorToBeResolved.")
                    expectedExceptionWasHit = true;
            }

            Assert.IsTrue(expectedExceptionWasHit, "Did not get the expected exception from AddValidatorToBeResolved.");
        }

        [Test]
        public void Throws_an_exception_if_attempts_to_resolve_a_validator_that_was_not_added()
        {
            var factory = CreateAServiceLocatorFactory();
            var typeForTesting = typeof (TestItem);

            var expectedExceptionWasHit = false;
            try
            {
                factory.GetValidator(typeForTesting);
            }
            catch (ArgumentException argumentException)
            {
                if (argumentException.Message == "The TestItem type was not registered with the validator factory.")
                    expectedExceptionWasHit = true;
            }

            Assert.IsTrue(expectedExceptionWasHit, "Did not get the expected exception from GetValidator.");
        }

        [Test]
        public void Generic__Can_resolve_a_validator_after_adding_the_type()
        {
            var serviceLocator = new TestServiceLocator();
            var factory = new ServiceLocatorValidatorFactory(serviceLocator);

            var validatorType = typeof (TestItemValidator);

            factory.AddValidatorToBeResolved(validatorType);

            factory.GetValidator<TestItem>();

            Assert.AreEqual(validatorType, serviceLocator.TypeThatWasResolved);
        }

        [Test]
        public void Generic__Resolves_the_correct_type_when_multiple_validators_are_added()
        {
            var serviceLocator = new TestServiceLocator();
            var factory = new ServiceLocatorValidatorFactory(serviceLocator);

            var validatorType = typeof (TestItemValidator);

            factory.AddValidatorToBeResolved(validatorType);
            factory.AddValidatorToBeResolved(typeof (SecondTestItemValidator));

            factory.GetValidator<TestItem>();

            Assert.AreEqual(validatorType, serviceLocator.TypeThatWasResolved);
        }

        [Test]
        public void Generic__Throws_an_exception_if_attempts_to_resolve_a_validator_that_was_not_added()
        {
            var factory = CreateAServiceLocatorFactory();

            var expectedExceptionWasHit = false;
            try
            {
                factory.GetValidator<TestItem>();
            }
            catch (ArgumentException argumentException)
            {
                if (argumentException.Message == "The TestItem type was not registered with the validator factory.")
                    expectedExceptionWasHit = true;
            }

            Assert.IsTrue(expectedExceptionWasHit, "Did not get the expected exception from GetValidator.");
        }

        private ServiceLocatorValidatorFactory CreateAServiceLocatorFactory()
        {
            var serviceLocator = new TestServiceLocator();
            return new ServiceLocatorValidatorFactory(serviceLocator);
        }

        public class TestServiceLocator : IServiceLocator
        {
            public Type TypeThatWasResolved { get; set; }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>() where T : class
            {
                TypeThatWasResolved = typeof (T);
                return null;
            }

            public T Resolve<T>(string key) where T : class
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>(Type type) where T : class
            {
                throw new NotImplementedException();
            }

            public object Resolve(Type type)
            {
                TypeThatWasResolved = type;
                return null;
            }

            public IList<T> ResolveServices<T>() where T : class
            {
                throw new NotImplementedException();
            }

            public IServiceRegistrar Batch()
            {
                throw new NotImplementedException();
            }

            public void Register<Interface>(Type implType) where Interface : class
            {
                throw new NotImplementedException();
            }

            public void Register<Interface, Implementation>() where Implementation : class, Interface
            {
                throw new NotImplementedException();
            }

            public void Register<Interface, Implementation>(string key) where Implementation : class, Interface
            {
                throw new NotImplementedException();
            }

            public void Register(string key, Type type)
            {
                throw new NotImplementedException();
            }

            public void Register(Type serviceType, Type implType)
            {
                throw new NotImplementedException();
            }

            public void Register<Interface>(Interface instance) where Interface : class
            {
                throw new NotImplementedException();
            }

            public void Release(object instance)
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public TService Inject<TService>(TService instance) where TService : class
            {
                throw new NotImplementedException();
            }

            public void TearDown<TService>(TService instance) where TService : class
            {
                throw new NotImplementedException();
            }
        }
    }
}