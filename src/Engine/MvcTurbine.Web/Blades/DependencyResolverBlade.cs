using System.Web.Mvc;
using MvcTurbine.Blades;
using MvcTurbine.Web.Controllers;

namespace MvcTurbine.Web.Blades
{
    public class DependencyResolverBlade : Blade
    {
        public override void Spin(IRotorContext context)
        {
            var dependencyResolver = GetTheDependencyResolver(context);

            DependencyResolver.SetResolver(dependencyResolver);
        }

        private IDependencyResolver GetTheDependencyResolver(IRotorContext context)
        {
            var serviceLocator = context.ServiceLocator;

            IDependencyResolver dependencyResolver;
            try
            {
                dependencyResolver = serviceLocator.Resolve<IDependencyResolver>();
            }
            catch
            {
                dependencyResolver = new TurbineDependencyResolver(serviceLocator);
            }
            return dependencyResolver;
        }
    }
}