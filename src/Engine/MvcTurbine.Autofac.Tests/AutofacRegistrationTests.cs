namespace MvcTurbine.Autofac.Tests {
    using ComponentModel;
    using ComponentModel.Tests;
    using NUnit.Framework;

    [TestFixture]
    public class AutofacRegistrationTests : RegistrationTests {
        protected override IServiceLocator CreateServiceLocator() {
            return new AutofacServiceLocator();
        }
    }
}