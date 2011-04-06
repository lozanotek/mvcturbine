namespace MvcTurbine.Unity {
    using Microsoft.Practices.Unity;
    using MvcTurbine.ComponentModel;

    /// <summary>
    /// A <see cref="IServiceRegistration"/> implementation that leverages the <see cref="IUnityContainer"/> from Unity.    
    /// </summary>
    public abstract class UnityRegistrator : IServiceRegistration {
        /// <summary>
        /// See <see cref="IServiceRegistration.Register"/>        
        /// </summary>
        /// <param name="locator"></param>
        public void Register(IServiceLocator locator) {
            Register(locator.GetUnderlyingContainer<IUnityContainer>());
        }

        /// <summary>
        /// See <see cref="IServiceRegistration.Register"/>
        /// </summary>
        /// <param name="container"></param>
        public abstract void Register(IUnityContainer container);
    }
}
