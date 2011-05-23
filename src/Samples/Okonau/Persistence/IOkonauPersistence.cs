namespace Okonau.Persistence {
    using NHibernate;

    /// <summary>
    /// Defines all operations for accessing database.
    /// </summary>
    public interface IOkonauPersistence {
        /// <summary>
        /// Gets the <see cref="IDatabaseResolver"/> for the current database.
        /// </summary>
        IDatabaseResolver CurrentDatabaseResolver { get; }

        /// <summary>
        /// Gets the current <see cref="ISessionFactory"/> to use.
        /// </summary>
        /// <returns></returns>
        ISessionFactory GetSessionFactory();

        /// <summary>
        /// Opens a new <see cref="ISession"/> to use.
        /// </summary>
        /// <returns></returns>
        ISession OpenSession();
        
        /// <summary>
        /// Gets the current <see cref="ISession"/> to use.
        /// </summary>
        /// <returns></returns>
        ISession GetCurrentSession();
    }
}
