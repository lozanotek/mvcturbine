namespace MvcTurbine.Windsor {
    using Castle.Windsor;
    using MvcTurbine.ComponentModel;

    /// <summary>
    /// A <see cref="IServiceRegistration"/> implementation that leverages the <see cref="IWindsorContainer"/> from Windsor.    
    /// </summary>
    public abstract class WindsorRegistrator : IServiceRegistration {
        /// <summary>
        /// See <see cref="IServiceRegistration.Register"/>        
        /// </summary>
        /// <param name="locator"></param>
        public void Register(IServiceLocator locator) {
            Register(locator.GetUnderlyingContainer<IWindsorContainer>());
        }

        /// <summary>
        /// See <see cref="IServiceRegistration.Register"/>
        /// </summary>
        /// <param name="container"></param>
        public abstract void Register(IWindsorContainer container);
    }
}
