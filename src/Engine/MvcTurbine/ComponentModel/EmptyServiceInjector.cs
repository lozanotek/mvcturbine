namespace MvcTurbine.ComponentModel
{
    public class EmptyServiceInjector : IServiceInjector
    {
        public TService Inject<TService>(TService instance) where TService : class
        {
            return instance;
        }

        public void TearDown<TService>(TService instance) where TService : class
        {
        }
    }
}