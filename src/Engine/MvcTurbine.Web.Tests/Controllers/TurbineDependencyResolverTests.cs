using System;
using System.Collections.Generic;
using System.Linq;
using MvcTurbine.ComponentModel;
using MvcTurbine.Web.Controllers;
using NUnit.Framework;

namespace MvcTurbine.Web.Tests.Controllers
{
    [TestFixture]
    public class TurbineDependencyResolverTests
    {
        [Test]
        public void GetService_returns_results_from_Resolve_call_on_serivce_locator()
        {
            var locator = new MockServiceLocator();

            var resolver  = new TurbineDependencyResolver(locator);
            
            var result = resolver.GetService(typeof(string));

            Assert.AreEqual("expected", result);
        }

        [Test]
        public void GetServices_returns_results_from_ResolveServices_call_on_service_locator()
        {
            var locator = new MockServiceLocator();

            var resolver = new TurbineDependencyResolver(locator);

            var results = resolver.GetServices(typeof(string));

            Assert.AreEqual(2, results.Count());            
        }

        [Test]
        public void ServiceLocator_returns_the_service_locator_passed_in_the_constructor()
        {
            var expected = new MockServiceLocator();

            var resolver = new TurbineDependencyResolver(expected);

            var locator = resolver.ServiceLocator;

            Assert.AreSame(expected, locator);
        }

        private class MockServiceLocator : IServiceLocator
        {
            public object Resolve(Type type)
            {
                if (type == typeof(string))
                    return "expected";
                throw new Exception();
            }

            public IList<object> ResolveServices(Type type)
            {
                if (type == typeof(string))
                    return (new[] {"one", "two"}).Cast<object>().ToList();
                throw new Exception();
            }

            #region Other MockServiceLocator methods

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>() where T : class
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

            #endregion
        }
    }
}