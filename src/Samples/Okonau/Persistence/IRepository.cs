namespace Okonau.Persistence {
    using System.Linq;

    /// <summary>
    /// Defines all operations for the repository to use.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IQueryable<T> {
        /// <summary>
        /// Adds an entity of type, <see cref="T"/> to the repository.
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Removes the entity of type, <see cref="T"/> from the repository.
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);
    }
}
