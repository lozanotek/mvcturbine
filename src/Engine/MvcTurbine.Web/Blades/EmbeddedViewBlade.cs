namespace MvcTurbine.Web.Blades {
    using System.Web.Hosting;
    using MvcTurbine.Blades;
    using ComponentModel;
    using Views;

    /// <summary>
    /// Defines the blade that deals with all the embedded views the system exposes.
    /// </summary>
    public class EmbeddedViewBlade : Blade {
        public override void Spin(IRotorContext context) {
            IServiceLocator serviceLocator = GetServiceLocatorFromContext(context);
            IEmbeddedViewResolver resolver = GetEmbeddedViewResolver(serviceLocator);

            EmbeddedViewTable table = resolver.GetEmbeddedViews();

            var embeddedProvider = new EmbeddedViewVirtualPathProvider(table);
            HostingEnvironment.RegisterVirtualPathProvider(embeddedProvider);
        }

        /// <summary>
        /// Gets the embedded view resolver for the system.
        /// </summary>
        /// <param name="serviceLocator"></param>
        /// <returns></returns>
        protected virtual IEmbeddedViewResolver GetEmbeddedViewResolver(IServiceLocator serviceLocator) {
            try {
                return serviceLocator.Resolve<IEmbeddedViewResolver>();
            }
            catch {
                return new EmbeddedViewResolver();
            }
        }
    }
}