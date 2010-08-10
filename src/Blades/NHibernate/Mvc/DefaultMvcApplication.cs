namespace Mvc {
	using Castle.MicroKernel.Registration;
	using Castle.MicroKernel.Resolvers.SpecializedResolvers;
	using Castle.Windsor;
	using Mappings;
	using MvcTurbine.ComponentModel;
	using MvcTurbine.Data;
	using MvcTurbine.NHibernate;
	using MvcTurbine.Windsor;
	using MvcTurbine.Web;
	using SomeModel;

	public class DefaultMvcApplication : TurbineApplication {
		//NOTE: You want to hit this piece of code only once.
		static DefaultMvcApplication() {
			ServiceLocatorManager.SetLocatorProvider(() => new WindsorServiceLocator(CreateContainer()));
		}

		static IWindsorContainer CreateContainer() {
			IWindsorContainer container = new WindsorContainer();

			// Add support for injection of arrays and IList<T> into
			// the components
			var kernel = container.Kernel;
			kernel.Resolver.AddSubResolver(new ArrayResolver(kernel));
			kernel.Resolver.AddSubResolver(new ListResolver(kernel));

			// Register the components into the container
			container.Register(Component.For<IRepository<Person>>()
				.ImplementedBy<GenericRepository<Person>>());

			// Persistence mappings
			container.Register(
				Component.For<ISessionProvider>()
						.ImplementedBy<SomeModelSessionProvider>(),

				Component.For<IDatabaseResolver>()
						.ImplementedBy<SqliteDatabase>());

			return container;
		}
	}
}
