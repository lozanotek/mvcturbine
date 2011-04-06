namespace MvcTurbine.Tests.ComponentModel {
    using System;
    using System.Collections.Generic;
    using MvcTurbine.ComponentModel;
    using NUnit.Framework;

    [TestFixture]
    public class DefaultAutoRegistratorTest {
        [Test]
        public void Can_Create_Valid_Instance_With_Null_Service() {
            var registrator = new DefaultAutoRegistrator(null);
            Assert.IsNotNull(registrator);
        }

        [Test]
        public void Can_Create_Valid_Instance_With_Valid_Service() {
            var serviceLocator = new MockServiceLocator();
            var registrator = new DefaultAutoRegistrator(serviceLocator);

            Assert.IsNotNull(registrator);
        }

        [Test]
        public void Create_With_Null_Service_And_Maintain_It() {
            var registrator = new DefaultAutoRegistrator(null);
            Assert.IsNull(registrator.ServiceLocator);
        }

        [Test]
        public void Create_With_Null_Service_And_Check_Filter() {
            var registrator = new DefaultAutoRegistrator(null);
            Assert.AreEqual(typeof(CommonAssemblyFilter), registrator.Filter.GetType());
            Assert.IsNotNull(registrator.Filter);
        }

        [Test]
        public void AutoRegister_With_Null_Registration_Returns_Automatically() {
            var registrator = new DefaultAutoRegistrator(null);
            registrator.AutoRegister(null);
        }

        [Test]
        public void AutoRegister_With_Invalid_Registration_Returns_Automatically() {
            var registrator = new DefaultAutoRegistrator(null);
            registrator.AutoRegister(new ServiceRegistration());
        }

        [Test]
        public void AutoRegister_With_Valid_Registration_And_Properties_Returns_Automatically() {
            var registrator = new DefaultAutoRegistrator(null);
            var count = 0;
            registrator.AutoRegister(new ServiceRegistration
            {
                RegistrationHandler = (locator, type) => { count++; },
                ServiceType = typeof(object),
                TypeFilter = (t1, t2) => true
            });

            Assert.Greater(count, 0);
        }

        [Test]
        public void AutoRegister_With_Valid_False_Registration_And_Properties_Returns_Automatically_() {
            var registrator = new DefaultAutoRegistrator(null);
            var count = 0;
            registrator.AutoRegister(new ServiceRegistration
            {
                RegistrationHandler = (locator, type) => { count++; },
                ServiceType = typeof(object),
                TypeFilter = (t1, t2) => false
            });

            Assert.AreEqual(count, 0);
        }
    }

    public class MockServiceLocator : IServiceLocator {
        #region IServiceLocator Members

        public void Dispose() {
            throw new NotImplementedException();
        }

        public T Resolve<T>() where T : class {
            throw new NotImplementedException();
        }

        public T Resolve<T>(string key) where T : class {
            throw new NotImplementedException();
        }

        public T Resolve<T>(Type type) where T : class {
            throw new NotImplementedException();
        }

        public object Resolve(Type type)
        {
            throw new NotImplementedException();
        }

        public IList<T> ResolveServices<T>() where T : class {
            throw new NotImplementedException();
        }

        public IList<object> ResolveServices(Type type)
        {
            throw new NotImplementedException();
        }

        public IServiceRegistrar Batch()
        {
            throw new NotImplementedException();
        }

        public void Register<Interface>(Type implType) where Interface : class {
            throw new NotImplementedException();
        }

        public void Register<Interface, Implementation>() where Implementation : class, Interface {
            throw new NotImplementedException();
        }

        public void Register<Interface, Implementation>(string key) where Implementation : class, Interface {
            throw new NotImplementedException();
        }

        public void Register(string key, Type type) {
            throw new NotImplementedException();
        }

        public void Register(Type serviceType, Type implType) {
            throw new NotImplementedException();
        }

        public void Register<Interface>(Interface instance) where Interface : class {
            throw new NotImplementedException();
        }

        public void Release(object instance) {
            throw new NotImplementedException();
        }

        public void Reset() {
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

        #endregion
    }
}