namespace MvcTurbine.Tests.ComponentModel {
    using System;
    using MvcTurbine.ComponentModel;
    using NUnit.Framework;

    [TestFixture]
    public class ServiceLocatorManagerTests {
        [Test]
        public void Empty_Provider_Return_Null_Provider() {
            ServiceLocatorManager.SetLocatorProvider(() => null);

            IServiceLocator locator = ServiceLocatorManager.Current;
            Assert.IsNull(locator);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Null_Provider_Throws_InvalidOperation_Exception() {
            ServiceLocatorManager.SetLocatorProvider(null);

            var locator = ServiceLocatorManager.Current;
        }
    }
}