namespace PocoSample.Mvc {
	using Castle.MicroKernel.Resolvers.SpecializedResolvers;
	using Castle.Windsor;
	using MvcTurbine.ComponentModel;
	using MvcTurbine.Windsor;
	using MvcTurbine.Web;

	public class DefaultMvcApplication : TurbineApplication {
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
			return container;
		}
	}
}
