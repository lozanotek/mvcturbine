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
