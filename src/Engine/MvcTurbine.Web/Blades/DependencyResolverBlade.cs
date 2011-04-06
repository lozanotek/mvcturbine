namespace MvcTurbine.Web.Blades {
    using System.Web.Mvc;
    using MvcTurbine.Blades;

    /// <summary>
    /// Blade for handling the registration of <see cref="IDependencyResolver"/>.
    /// </summary>
    public class DependencyResolverBlade : Blade {
        public override void Spin(IRotorContext context) {
            var dependencyResolver = GetDependencyResolver(context);
            DependencyResolver.SetResolver(dependencyResolver);
        }

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
