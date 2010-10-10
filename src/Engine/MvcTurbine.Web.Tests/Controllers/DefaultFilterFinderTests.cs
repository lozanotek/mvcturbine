//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using Moq;

//namespace MvcTurbine.Web.Tests.Controllers {
//    using ComponentModel;
//    using NUnit.Framework;
//    using Web.Controllers;

//    public class DefaultFilterFinderTests : TestFixtureBase {
//        [Test]
//        public void Create_New_Instance_With_Null_ServiceLocator() {
//            var instance = new DefaultFilterFinder(null);
//            Assert.IsNotNull(instance);
//            Assert.IsNull(instance.ServiceLocator);
//        }

//        [Test]
//        public void Create_New_Instance_With_Valid_ServiceLocator() {
//            DefaultFilterFinder finder;
//            var locator = new MockServiceLocator();

//            finder = new DefaultFilterFinder(locator);
//            Assert.IsNotNull(finder);
//            Assert.IsNotNull(finder.ServiceLocator);
//        }

//        [Test]
//        public void FindFilters_Returns_Registered_Action_Filters()
//        {
//            var expectedActionFilter = new Mock<IActionFilter>().Object;

//            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
//            locator.ReturnTheseClassesWhenUsingResolveServicesForThisType<IActionFilter>(new []{expectedActionFilter});

//            var finder = new DefaultFilterFinder(locator);
//            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

//            Assert.AreEqual(1, result.ActionFilters.Count);
//            Assert.AreSame(expectedActionFilter, result.ActionFilters.First());
//        }

//        [Test]
//        public void FindFilters_Does_Not_Include_Controllers_In_ActionFilter_Results()
//        {
//            var expectedActionFilter = new Mock<IActionFilter>().Object;
//            var actionFilters = new[] { new DefaultFilterFinderTestClasses.TestController(), expectedActionFilter,  };

//            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
//            locator.ReturnTheseClassesWhenUsingResolveServicesForThisType<IActionFilter>(actionFilters);

//            var finder = new DefaultFilterFinder(locator);
//            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

//            Assert.AreEqual(1, result.ActionFilters.Count);
//            Assert.AreSame(expectedActionFilter, result.ActionFilters.First()); 
//        }

//        [Test]
//        public void FindFilters_Returns_Registered_Authorization_Filters()
//        {
//            var expectedAuthorizationFilter = new Mock<IAuthorizationFilter>().Object;

//            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
//            locator.ReturnTheseClassesWhenUsingResolveServicesForThisType<IAuthorizationFilter>(new[] { expectedAuthorizationFilter });

//            var finder = new DefaultFilterFinder(locator);
//            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

//            Assert.AreEqual(1, result.AuthorizationFilters.Count);
//            Assert.AreSame(expectedAuthorizationFilter, result.AuthorizationFilters.First());
//        }

//        [Test]
//        public void FindFilters_Does_Not_Include_Controllers_In_Authorization_Results()
//        {
//            var expectedAuthorizationFilter = new Mock<IAuthorizationFilter>().Object;
//            var authorizationFilters = new[] { new DefaultFilterFinderTestClasses.TestController(), expectedAuthorizationFilter, };

//            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
//            locator.ReturnTheseClassesWhenUsingResolveServicesForThisType<IAuthorizationFilter>(authorizationFilters);

//            var finder = new DefaultFilterFinder(locator);
//            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

//            Assert.AreEqual(1, result.AuthorizationFilters.Count);
//            Assert.AreSame(expectedAuthorizationFilter, result.AuthorizationFilters.First());
//        }

//        [Test]
//        public void FindFilters_Returns_Registered_Exception_Filters()
//        {
//            var expectedExceptionFilter = new Mock<IExceptionFilter>().Object;

//            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
//            locator.ReturnTheseClassesWhenUsingResolveServicesForThisType<IExceptionFilter>(new[] { expectedExceptionFilter });

//            var finder = new DefaultFilterFinder(locator);
//            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

//            Assert.AreEqual(1, result.ExceptionFilters.Count);
//            Assert.AreSame(expectedExceptionFilter, result.ExceptionFilters.First());
//        }

//        [Test]
//        public void FindFilters_Does_Not_Include_Controllers_In_Exception_Results()
//        {
//            var expectedExceptionFilter = new Mock<IExceptionFilter>().Object;
//            var exceptionFilters = new[] { new DefaultFilterFinderTestClasses.TestController(), expectedExceptionFilter, };

//            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
//            locator.ReturnTheseClassesWhenUsingResolveServicesForThisType<IExceptionFilter>(exceptionFilters);

//            var finder = new DefaultFilterFinder(locator);
//            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

//            Assert.AreEqual(1, result.ExceptionFilters.Count);
//            Assert.AreSame(expectedExceptionFilter, result.ExceptionFilters.First());
//        }

//        [Test]
//        public void FindFilters_Returns_Registered_Result_Filters()
//        {
//            var expectedResultFilter = new Mock<IResultFilter>().Object;

//            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
//            locator.ReturnTheseClassesWhenUsingResolveServicesForThisType<IResultFilter>(new[] { expectedResultFilter });

//            var finder = new DefaultFilterFinder(locator);
//            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

//            Assert.AreEqual(1, result.ResultFilters.Count);
//            Assert.AreSame(expectedResultFilter, result.ResultFilters.First());
//        }

//        [Test]
//        public void FindFilters_Does_Not_Include_Controllers_In_Result_Results()
//        {
//            var expectedResultFilter = new Mock<IResultFilter>().Object;
//            var resultFilters = new[] { new DefaultFilterFinderTestClasses.TestController(), expectedResultFilter, };

//            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
//            locator.ReturnTheseClassesWhenUsingResolveServicesForThisType<IResultFilter>(resultFilters);

//            var finder = new DefaultFilterFinder(locator);
//            var result = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

//            Assert.AreEqual(1, result.ResultFilters.Count);
//            Assert.AreSame(expectedResultFilter, result.ResultFilters.First());
//        }

//        [Test]
//        public void FindFilters_Does_Not_Call_ResolveServices_Again_After_The_First_Call()
//        {
//            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
//            var finder = new DefaultFilterFinder(locator);

//            finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());
//            var countAfterFirstCall = locator.NumberOfTimesResolveServicesHasBeenCalled;

//            finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());
//            var countAfterSecondCall = locator.NumberOfTimesResolveServicesHasBeenCalled;

//            Assert.AreEqual(countAfterFirstCall, countAfterSecondCall);
//        }

//        [Test]
//        public void FindFilters_Returns_Filters_From_Resolve_After_First_Call()
//        {
//            var expectedResultFilter = new Mock<IResultFilter>().Object;

//            var locator = new DefaultFilterFinderTestClasses.TestServiceLocator();
//            locator.ReturnTheseClassesWhenUsingResolveServicesForThisType<IResultFilter>(new []{expectedResultFilter});
//            locator.ReturnThisClassWhenUsingResolveForThisType(expectedResultFilter);

//            var finder = new DefaultFilterFinder(locator);

//            // first call, should cache the types
//            finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());

//            //
//            var secondResult = finder.FindFilters(new DefaultFilterFinderTestClasses.TestActionDescriptor());


//            Assert.AreEqual(1, secondResult.ResultFilters.Count);
//            Assert.AreSame(expectedResultFilter, secondResult.ResultFilters.First());
//        }
//    }

//    public class DefaultFilterFinderTestClasses
//    {

//        public class TestActionDescriptor : ActionDescriptor
//        {
//            public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
//            {
//                throw new NotImplementedException();
//            }

//            public override ParameterDescriptor[] GetParameters()
//            {
//                throw new NotImplementedException();
//            }

//            public override string ActionName
//            {
//                get { throw new NotImplementedException(); }
//            }

//            public override ControllerDescriptor ControllerDescriptor
//            {
//                get { throw new NotImplementedException(); }
//            }
//        }

//        public class TestActionFilter : IActionFilter
//        {
//            public void OnActionExecuting(ActionExecutingContext filterContext)
//            {
//                throw new NotImplementedException();
//            }

//            public void OnActionExecuted(ActionExecutedContext filterContext)
//            {
//                throw new NotImplementedException();
//            }
//        }

//        public class TestController : Controller{}

//        #region service locator

//        public class TestServiceLocator : IServiceLocator
//        {
//            public Dictionary<Type, IEnumerable<object>> resolveServicesDictionary = new Dictionary<Type, IEnumerable<object>>();
//            public Dictionary<Type, object> resolveDictionary = new Dictionary<Type, object>();

//            public void ReturnThisClassWhenUsingResolveForThisType(object @object)
//            {
//                resolveDictionary.Add(@object.GetType(), @object);
//            }

//            public void ReturnTheseClassesWhenUsingResolveServicesForThisType<T>(IEnumerable<object> objects)
//            {
//                resolveServicesDictionary.Add(typeof (T), objects);
//            }

//            public int NumberOfTimesResolveServicesHasBeenCalled = 0;

//            public object Resolve(Type type)
//            {
//                if (resolveDictionary.ContainsKey(type))
//                    return resolveDictionary[type];
//                return null;
//            }

//            public IList<T> ResolveServices<T>() where T : class
//            {
//                NumberOfTimesResolveServicesHasBeenCalled++;
//                if (resolveServicesDictionary.ContainsKey(typeof(T)))
//                    return resolveServicesDictionary[typeof (T)].Cast<T>().ToList();
//                return new List<T>();
//            }

//            #region not implemented

//            public void Dispose()
//            {
//                throw new NotImplementedException();
//            }

//            public T Resolve<T>() where T : class
//            {
//                throw new NotImplementedException();
//            }

//            public T Resolve<T>(string key) where T : class
//            {
//                throw new NotImplementedException();
//            }

//            public T Resolve<T>(Type type) where T : class
//            {
//                throw new NotImplementedException();
//            }

//            public IServiceRegistrar Batch()
//            {
//                throw new NotImplementedException();
//            }

//            public void Register<Interface>(Type implType) where Interface : class
//            {
//                throw new NotImplementedException();
//            }

//            public void Register<Interface, Implementation>() where Implementation : class, Interface
//            {
//                throw new NotImplementedException();
//            }

//            public void Register<Interface, Implementation>(string key) where Implementation : class, Interface
//            {
//                throw new NotImplementedException();
//            }

//            public void Register(string key, Type type)
//            {
//                throw new NotImplementedException();
//            }

//            public void Register(Type serviceType, Type implType)
//            {
//                throw new NotImplementedException();
//            }

//            public void Register<Interface>(Interface instance) where Interface : class
//            {
//                throw new NotImplementedException();
//            }

//            public void Release(object instance)
//            {
//                throw new NotImplementedException();
//            }

//            public void Reset()
//            {
//                throw new NotImplementedException();
//            }

//            public TService Inject<TService>(TService instance) where TService : class
//            {
//                throw new NotImplementedException();
//            }

//            public void TearDown<TService>(TService instance) where TService : class
//            {
//                throw new NotImplementedException();
//            }
//            #endregion
//        }

//        #endregion
//    }
//}