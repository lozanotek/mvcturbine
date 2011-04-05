namespace Mvc {
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.Resolvers.SpecializedResolvers;
    using Castle.Windsor;
    using Mvc.Areas;
    using Mvc.Model;
    using Mvc.Services;
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Web;
    using MvcTurbine.Windsor;

    public class DefaultMvcApplication : TurbineApplication {
        //NOTE: You want to hit this piece of code only once.
        static DefaultMvcApplication() {
            ServiceLocatorManager.SetLocatorProvider(() => new WindsorServiceLocator(CreateContainer()));
        }

        private static IWindsorContainer CreateContainer() {
            IWindsorContainer container = new WindsorContainer();

            // Add support for injection of arrays and IList<T> into
            // the components
            IKernel kernel = container.Kernel;
            kernel.Resolver.AddSubResolver(new ArrayResolver(kernel));
            kernel.Resolver.AddSubResolver(new ListResolver(kernel));

            // Register the components into the container
            container.Register(Component.For<IMessageService>()
                                   .ImplementedBy<MessageService>());

            // Register a simple dependency for Areas
            container.Register(Component.For<IAreaDependency>()
                                   .ImplementedBy<SimpleAreaDependency>());

            // Register a simple dependency for Areas
            container.Register(Component.For<IValidatorProviderDependency>()
                                   .ImplementedBy<EmptyValidatorProviderDependency>());

            return container;
        }
    }
}
