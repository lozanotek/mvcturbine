namespace Mvc3Host.Modules {
    using MvcTurbine.Web.Modules;

    public sealed class MyModuleRegistry : HttpModuleRegistry {
        public MyModuleRegistry() {
            // This will add this module
            Add<MyModule>()

                // Will add same module with this specified
                .Add<MyModule>("anotherModule")

                // Will add yet again with different name
                .Add<MyModule>("yetAnotherModule");

            // Will remove the origin module.
            //.Remove<MyModule>();
        }
    }
}