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

namespace MvcTurbine.NHibernate {
	using System;
	using System.Linq;
	using global::NHibernate;
	using global::NHibernate.Linq;
	using Data;

	/// <summary>
	/// Simple repository for accessing entities via Linq to NHibernate.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class GenericRepository<T> : RepositoryBase<T> {
		/// <summary>
		/// Creates a new instance of the repository with the specified 
		/// <see cref="ISessionProvider"/> to use.
		/// </summary>
		/// <param name="provider"></param>
		public GenericRepository(ISessionProvider provider) {
			SessionProvider = provider;
		}

		/// <summary>
		/// Gets the <see cref="ISessionProvider"/> associated with this 
		/// repository.
		/// </summary>
		public virtual ISessionProvider SessionProvider { get; private set; }

		public override void Update(T entity) {
			ISession session = GetSession();
			session.Save(entity);
		}

		/// <summary>
		/// Provides the IQueryable adapter for Linq to NH.
		/// </summary>
		/// <returns></returns>
		public override IQueryable<T> LinqAdapter() {
			ISession session = GetSession();
			return session.Linq<T>();
		}

		/// <summary>
		/// Saves or updates the entity.
		/// </summary>
		/// <param name="entity">Entity to add/update.</param>
		public override void Add(T entity) {
			ISession session = GetSession();
			session.Save(entity);
		}

		/// <summary>
		/// Deletes the current entity.
		/// </summary>
		/// <param name="entity">Entity to delete.</param>
		public override void Remove(T entity) {
			ISession session = GetSession();
			session.Delete(entity);
		}

		/// <summary>
		/// Gets the current <see cref="ISession"/> for the context.
		/// </summary>
		/// <returns></returns>
		protected virtual ISession GetSession() {
			return SessionProvider.CurrentSession;
		}
	}
}
