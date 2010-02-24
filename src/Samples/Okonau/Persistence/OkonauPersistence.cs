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

namespace Okonau.Persistence {
    using System.IO;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using NHibernate;
    using NHibernate.ByteCode.Castle;
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;

    /// <summary>
    /// Default implementation of <see cref="IOkonauPersistence"/>.
    /// </summary>
    public class OkonauPersistence : IOkonauPersistence {
        private static readonly object _lock = new object();
        private static Configuration configuration;
        private static ISessionFactory sessionFactory;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="databaseResolver"></param>
        public OkonauPersistence(IDatabaseResolver databaseResolver) {
            CurrentDatabaseResolver = databaseResolver;
        }

        /// <summary>
        /// See <see cref="IOkonauPersistence.CurrentDatabaseResolver"/>.
        /// </summary>
        public IDatabaseResolver CurrentDatabaseResolver { get; private set; }

        /// <summary>
        /// Create the <see cref="Configuration"/> to use for the database.
        /// </summary>
        /// <returns></returns>
        private Configuration GetConfiguration() {
            lock (_lock) {
                if (configuration == null) {
                    lock (_lock) {
                        var filePath = CurrentDatabaseResolver.FilePath;
                        SQLiteConfiguration liteConfiguration =
                            SQLiteConfiguration.Standard
                                .UsingFile(filePath)
                                .ProxyFactoryFactory(typeof(ProxyFactoryFactory));

                        configuration =
                            Fluently
                                .Configure()
                                .Database(liteConfiguration)
                                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<OkonauPersistence>())
                                // Install the database if it doesn't exist
                                .ExposeConfiguration(config =>
                                {
                                    if (File.Exists(filePath)) return;

                                    SchemaExport export = new SchemaExport(config);
                                    export.Drop(false, true);
                                    export.Create(false, true);
                                })
                                .BuildConfiguration();


                        if (!configuration.Properties.ContainsKey("current_session_context_class")) {
                            configuration.Properties.Add("current_session_context_class", "managed_web");
                        }
                    }
                }

                return configuration;
            }
        }

        /// <summary>
        /// See <see cref="IOkonauPersistence.GetSessionFactory"/>.
        /// </summary>
        /// <returns></returns>
        public ISessionFactory GetSessionFactory() {
            if (sessionFactory == null) {
                var config = GetConfiguration();
                sessionFactory = config.BuildSessionFactory();
            }

            return sessionFactory;
        }

        /// <summary>
        /// See <see cref="IOkonauPersistence.OpenSession"/>.
        /// </summary>
        /// <returns></returns>
        public ISession OpenSession() {
            ISessionFactory factory = GetSessionFactory();
            return factory.OpenSession();
        }

        /// <summary>
        /// See <see cref="IOkonauPersistence.GetCurrentSession"/>.
        /// </summary>
        /// <returns></returns>
        public ISession GetCurrentSession() {
            ISessionFactory factory = GetSessionFactory();
            return factory.GetCurrentSession();
        }
    }
}
