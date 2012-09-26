namespace Mvc3Host.Models {
    using MvcTurbine.Web.Models;

    public class AppBinderRegistry : ModelBinderRegistry {
        public AppBinderRegistry() {
            Bind<Person, PersonBinder>();
        }
    }
}