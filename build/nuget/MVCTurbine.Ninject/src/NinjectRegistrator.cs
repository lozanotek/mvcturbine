namespace MvcTurbine.Ninject {
    using global::Ninject;
    using MvcTurbine.ComponentModel;

    /// <summary>
    /// A <see cref="IServiceRegistration"/> implementation that leverages the <see cref="IKernel"/> from Ninject.    
    /// </summary>
    public abstract class NinjectRegistrator : IServiceRegistration {
        /// <summary>
        /// See <see cref="IServiceRegistration.Register"/>        
        /// </summary>
        /// <param name="locator"></param>
        public void Register(IServiceLocator locator) {
            Register(locator.GetUnderlyingContainer<IKernel>());
        }

        /// <summary>
        /// See <see cref="IServiceRegistration.Register"/>
        /// </summary>
        /// <param name="kernel"></param>
        public abstract void Register(IKernel kernel);
    }
}
