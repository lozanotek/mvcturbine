namespace MvcTurbine.Web.Tests.Blades {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ComponentModel;

    internal class MockControllerFactoryServiceLocator : IServiceLocator {
        public bool ShouldThrowExceptionForControllerFactory { get; set; }
        public bool ShouldReturnNullForControllerFactory { get; set; }

        #region IServiceLocator Members

        public void Dispose() {
        }

        public IList<object> ResolveServices(Type type)
        {
            throw new NotImplementedException();
        }

        public IServiceRegistrar Batch() {
            throw new NotImplementedException();
        }

        public T Resolve<T>() where T : class {
            if (typeof(T) == typeof(IControllerFactory)) {

                if (ShouldThrowExceptionForControllerFactory) {
                    throw new Exception();
                }

                if (ShouldReturnNullForControllerFactory) {
                    return null;
                }

                return new DefaultControllerFactory() as T;
            }

            return null;
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