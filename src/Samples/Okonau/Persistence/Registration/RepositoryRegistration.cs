namespace Okonau.Persistence.Registration {
    using Domain;
    using MvcTurbine.ComponentModel;

    /// <summary>
    /// Defines registration of persistence pieces
    /// </summary>
    public class RepositoryRegistration : IServiceRegistration {
        public void Register(IServiceLocator locator) {
            locator.Register<IDatabaseResolver, SqliteDatabase>();
            locator.Register<IOkonauPersistence, OkonauPersistence>();
            locator.Register<IRepository<Task>, GenericRepository<Task>>();
        }
    }
}
