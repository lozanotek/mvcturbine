namespace Mvc3Host.Models {
    using MvcTurbine.Web.Models;

    public class FooRegistry : ModelBinderRegistry {
        public FooRegistry() {
            Bind<Foo, FooBinder>();
        }
    }
}