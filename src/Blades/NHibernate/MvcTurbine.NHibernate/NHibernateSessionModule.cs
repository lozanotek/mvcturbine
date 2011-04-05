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
	using global::NHibernate;
	using global::NHibernate.Context;
	using ComponentModel;

	[Serializable]
	public class SessionProviderException : Exception {
		public SessionProviderException(ISessionProvider provider, string message)
			: base(message) {
			Provider = provider;
		}

		public SessionProviderException(ISessionProvider provider,
			string message, Exception innerException)
			: base(message, innerException) {

			Provider = provider;
		}

		public ISessionProvider Provider { get; private set; }
	}

	[Serializable]
	public class SessionInitializationException : Exception {
		public SessionInitializationException(string message)
			: base(message) {
		}

		public SessionInitializationException(string message, Exception innerException)
			: base(message, innerException) {
		}
	}

	/// <summary>
	/// Module to handle per-request binding of <see cref="ISession"/>
	/// provided via the registered <see cref="ISessionProvider"/> types.
	/// </summary>
	public class NHibernateSessionModule : IHttpModule {
		private static IList<ISessionProvider> builtList;
		private static readonly object _lock = new object();

		/// <summary>
		/// Default constructor for the module
		/// </summary>
		/// <param name="serviceLocator"></param>
		public NHibernateSessionModule(IServiceLocator serviceLocator) {
			ServiceLocator = serviceLocator;
		}

		/// <summary>
		/// Gets the <see cref="IServiceLocator"/> for the application.
		/// </summary>
		public IServiceLocator ServiceLocator { get; private set; }

		/// <summary>
		/// Gets the list of registered <see cref="ISessionProvider"/> with the <see cref="IServiceLocator"/>.
		/// </summary>
		/// <returns></returns>
		public virtual IList<ISessionProvider> GetSessionProviders() {
			if (builtList == null) {
				lock (_lock) {
					if (builtList == null) {
						try {
							builtList = ServiceLocator.ResolveServices<ISessionProvider>();
						} catch (Exception ex) {
							var header = "Could not instantiate all the registered ISessionProvider types.";
							var message = string.Format("{0}  Inner exception -- \r\n {1}", header, ex.Message);

							throw new SessionInitializationException(message, ex);
						}
					}
				}
			}

			return builtList;
		}

		/// <summary>
		/// Wires up the event handlers for the request
		/// </summary>
		/// <param name="context"></param>
		public void Init(HttpApplication context) {
			context.BeginRequest += BeginRequest;
			context.EndRequest += EndRequest;
		}

		public void Dispose() {
		}

		/// <summary>
		/// Handles the <see cref="HttpApplication.BeginRequest"/> event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public virtual void BeginRequest(object sender, EventArgs e) {
			BindSession(HttpContext.Current);
		}

		/// <summary>
		/// Handles the <see cref="HttpApplication.EndRequest"/> event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public virtual void EndRequest(object sender, EventArgs e) {
			UnbindSession(HttpContext.Current);
		}

		/// <summary>
		/// Binds the list of <see cref="ISessionProvider"/> to session context for NHibernate.
		/// </summary>
		/// <param name="context"></param>
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

		/// <summary>
		/// Unbinds the <see cref="ISession"/> associated with the current request.
		/// </summary>
		/// <param name="context"></param>
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
