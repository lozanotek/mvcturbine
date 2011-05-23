namespace Okonau.Persistence {
    using System.IO;
    using System.Web;

    /// <summary>
    /// Default implementation of <see cref="IDatabaseResolver"/>.
    /// </summary>
    internal class SqliteDatabase : IDatabaseResolver {
        public string FilePath {
            get {
                string path = HttpRuntime.AppDomainAppPath;
                string dataPath = Path.Combine(path, "Data\\Okonau.db3");

                return dataPath;
            }
        }
    }
}