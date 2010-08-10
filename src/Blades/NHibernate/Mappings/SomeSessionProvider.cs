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
	using NHibernate;
	using NHibernate.Cfg;

	public class SomeSessionProvider : SimpleSessionProvider {
		private static readonly object _lock = new object();
		private static Configuration configuration;
		private static ISessionFactory sessionFactory;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public SomeSessionProvider(PersonDatabase database) {
			Database = database;
		}

		public PersonDatabase Database { get; private set; }

		public override Configuration BuildConfiguration() {
			lock (_lock) {
				if (configuration == null) {
					lock (_lock) {
						configuration = FluentlyConfigureSqlite(Database);
					}
				}

				return configuration;
			}
		}

		public override ISessionFactory GetSessionFactory() {
			lock (_lock) {
				if (sessionFactory == null) {
					lock (_lock) {
						var config = BuildConfiguration();
						sessionFactory = config.BuildSessionFactory();
					}
				}

				return sessionFactory;
			}
		}
	}
}
