namespace MvcTurbine.Web.Blades {
    using System.Web.Mvc;
    using MvcTurbine.Blades;

    /// <summary>
    /// Blade for handling the registration of <see cref="IDependencyResolver"/>.
    /// </summary>
    public class DependencyResolverBlade : CoreBlade {
        public override void Spin(IRotorContext context) {
            var dependencyResolver = GetDependencyResolver(context);
            DependencyResolver.SetResolver(dependencyResolver);
        }

		/// <summary>
		/// Gets the registered <see cref="IDependencyResolver"/> that's configured with the system.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
        protected virtual IDependencyResolver GetDependencyResolver(IRotorContext context) {
            var serviceLocator = context.ServiceLocator;

            try {
                return serviceLocator.Resolve<IDependencyResolver>();
            } catch {
                return new TurbineDependencyResolver(serviceLocator);
            }
        }
    }
}
