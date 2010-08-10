#region License

//
// Author: Javier Lozano <javier@lozanotek.com>
// Copyright (c) 2009-2010, lozanotek, inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

namespace Mappings {
	using System.IO;
	using FluentNHibernate.Cfg;
	using FluentNHibernate.Cfg.Db;
	using MvcTurbine.NHibernate;
	using NHibernate;
	using NHibernate.ByteCode.Castle;
	using NHibernate.Cfg;
	using NHibernate.Tool.hbm2ddl;

	public class SomeModelSessionProvider : SessionProvider {
		private static readonly object _lock = new object();
		private static Configuration configuration;
		private static ISessionFactory sessionFactory;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="databaseResolver"></param>
		public SomeModelSessionProvider(IDatabaseResolver databaseResolver) {
			CurrentDatabaseResolver = databaseResolver;
		}

		public IDatabaseResolver CurrentDatabaseResolver { get; private set; }

		public override Configuration GetConfiguration() {
			lock (_lock) {
				if (configuration == null) {
					lock (_lock) {
						BuildConfiguration();
					}
				}

				return configuration;
			}
		}

		public override ISessionFactory GetSessionFactory() {
			lock (_lock) {
				if (sessionFactory == null) {
					lock (_lock) {
						var config = GetConfiguration();
						sessionFactory = config.BuildSessionFactory();
					}
				}

				return sessionFactory;
			}
		}

		private void BuildConfiguration() {
			var filePath = CurrentDatabaseResolver.FilePath;
			SQLiteConfiguration liteConfiguration =
				SQLiteConfiguration.Standard
					.UsingFile(filePath)
					.ProxyFactoryFactory(typeof(ProxyFactoryFactory));

			configuration =
				Fluently
					.Configure()
					.Database(liteConfiguration)
					.Mappings(m => m.FluentMappings.AddFromAssemblyOf<SomeModelSessionProvider>())
				// Install the database if it doesn't exist
					.ExposeConfiguration(config =>
					{
						if (File.Exists(filePath)) return;

						SchemaExport export = new SchemaExport(config);
						export.Drop(false, true);
						export.Create(false, true);
					})
					.BuildConfiguration();

			AddProperties(configuration);
		}
	}
}
