namespace StructureMapExtensions {
    using MvcTurbine.ComponentModel;
    using StructureMap;

    public abstract class StructureMapRegistrator : IServiceRegistration {
        public void Register(IServiceLocator locator) {
            Register(locator.GetUnderlyingContainer<Container>());
        }

        public abstract void Register(IContainer container);
    }
}