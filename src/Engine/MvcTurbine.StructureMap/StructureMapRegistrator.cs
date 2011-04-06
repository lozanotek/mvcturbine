namespace MvcTurbine.StructureMap {
    using global::StructureMap;
    using MvcTurbine.ComponentModel;

    /// <summary>
    /// A <see cref="IServiceRegistration"/> implementation that leverages the <see cref="IContainer"/> from StructureMap.
    /// </summary>
    public abstract class StructureMapRegistrator : IServiceRegistration {
        /// <summary>
        /// See <see cref="IServiceRegistration.Register"/>        
        /// </summary>
        /// <param name="locator"></param>
        public void Register(IServiceLocator locator) {
            Register(locator.GetUnderlyingContainer<IContainer>());
        }

        /// <summary>
        /// See <see cref="IServiceRegistration.Register"/>
        /// </summary>
        /// <param name="container"></param>
        public abstract void Register(IContainer container);
    }
}