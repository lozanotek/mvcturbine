namespace Okonau.Persistence {
    /// <summary>
    /// Defines operations for the resolution of the database.
    /// </summary>
    public interface IDatabaseResolver {
        /// <summary>
        /// Gets the path of the file to use.
        /// </summary>
        string FilePath { get; }
    }
}
