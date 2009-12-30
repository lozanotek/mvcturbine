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

            var foo = Get<IServiceLocator>();
            finder = new DefaultFilterFinder(locator);
            Assert.IsNotNull(finder);
            Assert.IsNotNull(finder.ServiceLocator);
        }
    }
}