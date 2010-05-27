using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;

namespace MvcTurbine.Web.Tests.Controllers {
    using ComponentModel;
    using NUnit.Framework;
    using Web.Controllers;

    public class DefaultFilterFinderTests : TestFixtureBase {
        [Test]
        public void Create_New_Instance_With_Null_ServiceLocator() {
            var instance = new DefaultFilterFinder(null);
            Assert.IsNotNull(instance);
            Assert.IsNull(instance.ServiceLocator);
        }

        [Test]
        public void Create_New_Instance_With_Valid_ServiceLocator() {
            DefaultFilterFinder finder;
            var locator = new MockServiceLocator();

            finder = new DefaultFilterFinder(locator);
            Assert.IsNotNull(finder);
            Assert.IsNotNull(finder.ServiceLocator);
        }

        [Test]
        public void FindFilters_Returns_Registered_Action_Filters()
        {
            var expectedActionFilter = new Mock<IActionFilter>().Object;

            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
            locator.ReturnTheseClassesWhenResolvingThisType<IActionFilter>(new []{expectedActionFilter});

            var finder = new DefaultFilterFinder(locator);
            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

            Assert.AreEqual(1, result.ActionFilters.Count);
            Assert.AreSame(expectedActionFilter, result.ActionFilters.First());
        }

        [Test]
        public void FindFilters_Does_Not_Include_Controllers_In_ActionFilter_Results()
        {
            var expectedActionFilter = new Mock<IActionFilter>().Object;
            var actionFilters = new[] { new DefaultFilterFinderTestClasses.TestController(), expectedActionFilter,  };

            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
            locator.ReturnTheseClassesWhenResolvingThisType<IActionFilter>(actionFilters);

            var finder = new DefaultFilterFinder(locator);
            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

            Assert.AreEqual(1, result.ActionFilters.Count);
            Assert.AreSame(expectedActionFilter, result.ActionFilters.First()); 
        }

        [Test]
        public void FindFilters_Returns_Registered_Authorization_Filters()
        {
            var expectedAuthorizationFilter = new Mock<IAuthorizationFilter>().Object;

            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
            locator.ReturnTheseClassesWhenResolvingThisType<IAuthorizationFilter>(new[] { expectedAuthorizationFilter });

            var finder = new DefaultFilterFinder(locator);
            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

            Assert.AreEqual(1, result.AuthorizationFilters.Count);
            Assert.AreSame(expectedAuthorizationFilter, result.AuthorizationFilters.First());
        }

        [Test]
        public void FindFilters_Does_Not_Include_Controllers_In_Authorization_Results()
        {
            var expectedAuthorizationFilter = new Mock<IAuthorizationFilter>().Object;
            var authorizationFilters = new[] { new DefaultFilterFinderTestClasses.TestController(), expectedAuthorizationFilter, };

            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
            locator.ReturnTheseClassesWhenResolvingThisType<IAuthorizationFilter>(authorizationFilters);

            var finder = new DefaultFilterFinder(locator);
            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

            Assert.AreEqual(1, result.AuthorizationFilters.Count);
            Assert.AreSame(expectedAuthorizationFilter, result.AuthorizationFilters.First());
        }

        [Test]
        public void FindFilters_Returns_Registered_Exception_Filters()
        {
            var expectedExceptionFilter = new Mock<IExceptionFilter>().Object;

            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
            locator.ReturnTheseClassesWhenResolvingThisType<IExceptionFilter>(new[] { expectedExceptionFilter });

            var finder = new DefaultFilterFinder(locator);
            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

            Assert.AreEqual(1, result.ExceptionFilters.Count);
            Assert.AreSame(expectedExceptionFilter, result.ExceptionFilters.First());
        }

        [Test]
        public void FindFilters_Does_Not_Include_Controllers_In_Exception_Results()
        {
            var expectedExceptionFilter = new Mock<IExceptionFilter>().Object;
            var exceptionFilters = new[] { new DefaultFilterFinderTestClasses.TestController(), expectedExceptionFilter, };

            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
            locator.ReturnTheseClassesWhenResolvingThisType<IExceptionFilter>(exceptionFilters);

            var finder = new DefaultFilterFinder(locator);
            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

            Assert.AreEqual(1, result.ExceptionFilters.Count);
            Assert.AreSame(expectedExceptionFilter, result.ExceptionFilters.First());
        }

        [Test]
        public void FindFilters_Returns_Registered_Result_Filters()
        {
            var expectedResultFilter = new Mock<IResultFilter>().Object;

            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
            locator.ReturnTheseClassesWhenResolvingThisType<IResultFilter>(new[] { expectedResultFilter });

            var finder = new DefaultFilterFinder(locator);
            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

            Assert.AreEqual(1, result.ResultFilters.Count);
            Assert.AreSame(expectedResultFilter, result.ResultFilters.First());
        }

        [Test]
        public void FindFilters_Does_Not_Include_Controllers_In_Result_Results()
        {
            var expectedResultFilter = new Mock<IResultFilter>().Object;
            var resultFilters = new[] { new DefaultFilterFinderTestClasses.TestController(), expectedResultFilter, };

            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
            locator.ReturnTheseClassesWhenResolvingThisType<IResultFilter>(resultFilters);

            var finder = new DefaultFilterFinder(locator);
            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

            Assert.AreEqual(1, result.ResultFilters.Count);
            Assert.AreSame(expectedResultFilter, result.ResultFilters.First());
        }

    }

    public class DefaultFilterFinderTestClasses
    {

        public class TestActionDescriptor : ActionDescriptor
        {
            public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
            {
                throw new NotImplementedException();
            }

            public override ParameterDescriptor[] GetParameters()
            {
                throw new NotImplementedException();
            }

            public override string ActionName
            {
                get { throw new NotImplementedException(); }
            }

            public override ControllerDescriptor ControllerDescriptor
            {
                get { throw new NotImplementedException(); }
            }
        }

        public class TestActionFilter : IActionFilter
        {
            public void OnActionExecuting(ActionExecutingContext filterContext)
            {
                throw new NotImplementedException();
            }

            public void OnActionExecuted(ActionExecutedContext filterContext)
            {
                throw new NotImplementedException();
            }
        }

        public class TestController : Controller{}

        #region service locator

        public class TestServiceLocator : IServiceLocator
        {

            public Dictionary<Type, IEnumerable<object>> typeDictionary = new Dictionary<Type, IEnumerable<object>>();

            public void ReturnTheseClassesWhenResolvingThisType<T>(IEnumerable<object> objects)
            {
                typeDictionary.Add(typeof (T), objects);
            }

            public IList<T> ResolveServices<T>() where T : class
            {
                if (typeDictionary.ContainsKey(typeof(T)))
                    return typeDictionary[typeof (T)].Cast<T>().ToList();
                return new List<T>();
            }

            #region not implemented

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

            public object Resolve(Type type)
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

        #endregion
    }
}