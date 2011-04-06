namespace MvcTurbine.Tests.ComponentModel {
    using System;
    using MvcTurbine.ComponentModel;
    using NUnit.Framework;

    [TestFixture]
    public class ServiceResolutionExceptionTests {
        [Test]
        [ExpectedException(typeof (ServiceResolutionException))]
        public void Create_Instance_With_Type() {
            var exception = new ServiceResolutionException(typeof (string));
            Assert.AreEqual(exception.ServiceType, typeof (string));
            Assert.AreEqual(exception.Message, "Could not resolve serviceType 'System.String'");

            throw exception;
        }

        [Test]
        [ExpectedException(typeof (ServiceResolutionException))]
        public void Create_Instance_With_Type_And_InnerException() {
            var operationException = new InvalidOperationException();
            var exception = new ServiceResolutionException(typeof (string), operationException);
            
            Assert.AreEqual(exception.ServiceType, typeof (string));
            Assert.AreEqual(exception.InnerException, operationException);
            Assert.AreEqual(exception.Message, "Could not resolve serviceType 'System.String'");

            throw exception;
        }
    }
}