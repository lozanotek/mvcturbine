namespace Mappings {
	using System.IO;
	using FluentNHibernate.Cfg;
	using FluentNHibernate.Cfg.Db;
	using MvcTurbine.NHibernate;
	using NHibernate.ByteCode.Castle;
	using NHibernate.Cfg;
	using NHibernate.Tool.hbm2ddl;

	public abstract class SimpleSessionProvider : SessionProvider {
		protected virtual Configuration FluentlyConfigureSqlite(SqliteDatabase database) {
			var filePath = database.FilePath;
			SQLiteConfiguration liteConfiguration =
				SQLiteConfiguration.Standard
					.UsingFile(filePath)
					.ProxyFactoryFactory(typeof(ProxyFactoryFactory));

			var fluentConfig =
				Fluently
					.Configure()
					.Database(liteConfiguration)
					.Mappings(m => m.FluentMappings.AddFromAssembly(GetType().Assembly))
					// Install the database if it doesn't exist
					.ExposeConfiguration(config =>
					{
						if (File.Exists(filePath)) return;

						SchemaExport export = new SchemaExport(config);
						export.Drop(false, true);
						export.Create(false, true);
					})
					.BuildConfiguration();

			AddProperties(fluentConfig);

			return fluentConfig;
		}
	}
}
