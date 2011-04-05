namespace Mappings {
	public class TaskDatabase : SqliteDatabase {
		public override string DbName {
			get { return "Task"; }
		}
	}
}