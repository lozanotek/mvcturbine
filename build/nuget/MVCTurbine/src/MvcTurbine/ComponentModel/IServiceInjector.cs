namespace MvcTurbine.ComponentModel {
    /// <summary>
    /// Provides the contract for injecting dependency into an object not created by the underlying container.
    /// </summary>
    public interface IServiceInjector {
        /// <summary>
        /// Injects any types that are registered into the specified <paramref name="instance"/>.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="instance"></param>
        TService Inject<TService>(TService instance) where TService : class;

        /// <summary>
        /// Releases any types that have been registered into the specified <paramref name="instance"/>.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="instance"></param>
        void TearDown<TService>(TService instance) where TService : class;        
    }
}
