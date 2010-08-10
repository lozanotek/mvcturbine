namespace Mappings {
	using System.IO;
	using System.Web;

	public abstract class SqliteDatabase {
		public string FilePath {
			get {
				string path = HttpRuntime.AppDomainAppPath;
				var fileName = string.Format("Data\\{0}.db3", DbName ?? "Data");
				string dataPath = Path.Combine(path, fileName);

				return dataPath;
			}
		}

		public abstract string DbName { get; }
	}
}