namespace MvcTurbine.Autofac.Tests {
    using ComponentModel;
    using ComponentModel.Tests;
    using NUnit.Framework;

    [TestFixture]
    public class AutofacRegistrationTests : RegistrationTests {
        protected override IServiceLocator CreateServiceLocator() {
            return new AutofacServiceLocator();
        }


        [Test]
        public void Registering_with_implementation_should_override_the_previous_registration()
        {
            locator.Register<IRepository, ProductRepository>();
            locator.Register<IRepository, AccountRepository>();
            var instance = locator.Resolve<IRepository>();
            Assert.IsInstanceOfType(typeof(AccountRepository), instance);
        }

        [Test]
        public void Registering_with_implementation_should_override_the_previous_registration_when_resolve_is_called_inbetween()
        {
            locator.Register<IRepository, ProductRepository>();
            locator.Resolve<IRepository>();
            locator.Register<IRepository, AccountRepository>();
            var instance = locator.Resolve<IRepository>();
            Assert.IsInstanceOfType(typeof(AccountRepository), instance);
        }

        [Test]
        public void Registering_with_specified_type_should_override_the_previous_registration_when_resolve_is_called_inbetween()
        {
            locator.Register<IRepository>(typeof (ProductRepository));
            locator.Resolve<IRepository>();
            locator.Register<IRepository>(typeof (AccountRepository));
            var instance = locator.Resolve<IRepository>();
            Assert.IsInstanceOfType(typeof(AccountRepository), instance);
        }

        [Test]
        public void Registering_with_keyed_implementation_should_override_the_previous_registration_when_resolve_is_called_inbetween()
        {
            locator.Register<IRepository, ProductRepository>("key");
            locator.Resolve<IRepository>("key");
            locator.Register<IRepository, AccountRepository>("key");
            var instance = locator.Resolve<IRepository>("key");
            Assert.IsInstanceOfType(typeof(AccountRepository), instance);            
        }

        [Test]
        public void Registering_with_specified_service_and_type_should_override_the_previous_registration_when_resolve_is_called_inbetween()
        {
            locator.Register(typeof(IRepository), typeof(ProductRepository));
            locator.Resolve<IRepository>();
            locator.Register(typeof(IRepository), typeof(AccountRepository));
            var instance = locator.Resolve<IRepository>();
            Assert.IsInstanceOfType(typeof(AccountRepository), instance);
        }

        [Test]
        public void Register_with_instance_should_override_the_previous_registration_when_resolve_is_called_inbetween()
        {
            locator.Register<IRepository>(new ProductRepository());
            locator.Resolve<IRepository>();
            locator.Register<IRepository>(new AccountRepository());
            var instance = locator.Resolve<IRepository>();
            Assert.IsInstanceOfType(typeof(AccountRepository), instance);
        }

        #region fakes for testing

        private interface IRepository { }

        private class ProductRepository : IRepository { }

        private class AccountRepository : IRepository { }

        #endregion

    }
}