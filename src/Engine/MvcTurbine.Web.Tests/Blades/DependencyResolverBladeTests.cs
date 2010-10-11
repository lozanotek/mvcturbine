using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using MvcTurbine.ComponentModel;
using MvcTurbine.Web.Blades;
using MvcTurbine.Web.Controllers;
using NUnit.Framework;

namespace MvcTurbine.Web.Tests.Blades
{
    [TestFixture]
    public class DependencyResolverBladeTests
    {
        [Test]
        public void Sets_the_dependency_resolver_to_an_instance_of_TurbineDependencyResolver_when_no_IDependencyResolver_is_registered()
        {
            var blade = new DependencyResolverBlade();

            var serviceLocator = new MockServiceLocator();
            serviceLocator.ReturnAResolutionErrorWhenResolving();

            var fakeRotorContext = CreateRotorContextWithThisServiceLocator(serviceLocator);
            blade.Spin(fakeRotorContext);

            Assert.IsInstanceOfType(typeof (TurbineDependencyResolver), DependencyResolver.Current);
        }

        [Test]
        public void Passes_the_service_locator_from_rotorcontext_to_the_TurbineDependencyResolver()
        {
            var expected = new MockServiceLocator();
            expected.ReturnAResolutionErrorWhenResolving();

            var blade = new DependencyResolverBlade();

            var fakeRotorContext = CreateRotorContextWithThisServiceLocator(expected);
            blade.Spin(fakeRotorContext);

            Assert.AreSame(expected, ((TurbineDependencyResolver) DependencyResolver.Current).ServiceLocator);
        }

        [Test]
        public void Sets_the_current_IDependencyResolver_to_one_from_the_service_locator()
        {
            var expected = new Mock<IDependencyResolver>().Object;

            var locator = new MockServiceLocator();
            locator.ReturnThisDependencyResolver(expected);

            var blade = new DependencyResolverBlade();

            var fakeRotorContext = CreateRotorContextWithThisServiceLocator(locator);
            blade.Spin(fakeRotorContext);

            Assert.AreSame(expected, DependencyResolver.Current);
        }

        private IRotorContext CreateRotorContextWithThisServiceLocator(MockServiceLocator serviceLocator)
        {
            var fakeRotorContext = new Mock<IRotorContext>();
            fakeRotorContext.Setup(x => x.ServiceLocator)
                .Returns(serviceLocator);
            return fakeRotorContext.Object;
        }

        public class MockServiceLocator : IServiceLocator
        {
            private IDependencyResolver dependencyResolverToUse;

            public void ReturnAResolutionErrorWhenResolving()
            {
                dependencyResolverToUse = null;
            }

            public T Resolve<T>() where T : class
            {
                if (dependencyResolverToUse == null)
                    throw new ServiceResolutionException(typeof (T));
                return dependencyResolverToUse as T;
            }

            public void ReturnThisDependencyResolver(IDependencyResolver dependencyResolver)
            {
                dependencyResolverToUse = dependencyResolver;
            }

            #region Methods

            public void Dispose()
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }

            public IList<T> ResolveServices<T>() where T : class
            {
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

            #endregion
        }
    }
}