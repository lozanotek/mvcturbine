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
	using System.Collections.Generic;
	using System.Web;
	using global::NHibernate.Context;
	using ComponentModel;

	public class NHibernateSessionModule : IHttpModule {
		private static IList<ISessionProvider> builderList;
		private static readonly object _lock = new object();

		public IServiceLocator ServiceLocator { get; private set; }

		public NHibernateSessionModule(IServiceLocator serviceLocator) {
			ServiceLocator = serviceLocator;
		}

		public virtual IList<ISessionProvider> GetSessionProviders() {
			if (builderList == null) {
				lock (_lock) {
					if (builderList == null) {
						try {
							builderList = ServiceLocator.ResolveServices<ISessionProvider>();
						} catch (Exception) {
							return null;
						}
					}
				}
			}

			return builderList;
		}

		public void Init(HttpApplication context) {
			context.BeginRequest += BeginRequest;
			context.EndRequest += EndRequest;
		}

		public void Dispose() {
		}

		public virtual void BeginRequest(object sender, EventArgs e) {
			BindSession(HttpContext.Current);
		}

		public virtual void EndRequest(object sender, EventArgs e) {
			UnbindSession(HttpContext.Current);
		}

		protected virtual void BindSession(HttpContext context) {
			var providerList = GetSessionProviders();
			if (providerList == null || providerList.Count == 0) return;

			foreach (var provider in providerList) {
				// Create a new session (it's the beginning of the request)
				var session = provider.OpenSession();

				// HACK: To handle the ISessionBuilder resolution pieces.
				if (session == null) continue;

				ManagedWebSessionContext.Bind(context, session);
			}
		}

		protected virtual void UnbindSession(HttpContext context) {
			var providerList = GetSessionProviders();
			if (providerList == null || providerList.Count == 0) return;

			foreach (var provider in providerList) {
				// Get the default NH session factory
				var factory = provider.GetSessionFactory();

				var session = ManagedWebSessionContext.Unbind(context, factory);

				// Give it to NH so it can pull the right session
				if (session == null) return;

				using (session) {
					session.Flush();
				}
			}
		}
	}
}
