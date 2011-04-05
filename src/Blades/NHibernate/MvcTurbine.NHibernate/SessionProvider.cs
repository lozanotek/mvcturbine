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
	using global::NHibernate;
	using global::NHibernate.Cfg;

	/// <summary>
	/// Base implementation of <see cref="ISessionProvider"/> that wires up the 
	/// pieces for simple consumption of NHiberate.
	/// </summary>
	public abstract class SessionProvider : ISessionProvider {
		/// <summary>
		/// See <see cref="ISessionProvider.CurrentSession"/>
		/// </summary>
		public virtual ISession CurrentSession {
			get {
				// Get the default SessionFactory
				var factory = GetSessionFactory();

				//Let NH handle the 'churn' for you
				return factory.GetCurrentSession();
			}
		}

		/// <summary>
		/// Gets the type of session context that should be used for storing
		/// the <see cref="ISession"/> instance. Default is Managed Web.
		/// </summary>
		public virtual string SessionContextName {
			get {
				// for more information, 
				// see http://nhforge.org/wikis/reference2-0en/context-sessions.aspx
				return "managed_web";
			}
		}

		/// <summary>
		/// See <see cref="ISessionProvider.BuildConfiguration"/>
		/// </summary>
		/// <returns></returns>
		public abstract Configuration BuildConfiguration();

		/// <summary>
		/// See <see cref="ISessionProvider.GetSessionFactory"/>
		/// </summary>
		/// <returns></returns>
		public abstract ISessionFactory GetSessionFactory();

		/// <summary>
		/// See <see cref="ISessionProvider.OpenSession"/>
		/// </summary>
		/// <returns></returns>
		public virtual ISession OpenSession() {
			ISessionFactory factory = GetSessionFactory();
			return factory.OpenSession();
		}

		/// <summary>
		/// See <see cref="ISessionProvider.OpenStatelessSession"/>
		/// </summary>
		/// <returns></returns>
		public virtual IStatelessSession OpenStatelessSession() {
			var factory = GetSessionFactory();
			return factory.OpenStatelessSession();
		}

		/// <summary>
		/// Adds properties to the configuration object. Default property added is
		/// <see cref="SessionContextName"/> as the Session Context class to use.
		/// </summary>
		/// <param name="configuration"></param>
		protected virtual void AddProperties(Configuration configuration) {
			if (!configuration.Properties.ContainsKey("current_session_context_class")) {
				configuration.SetProperty("current_session_context_class", SessionContextName);
			}
		}
	}
}
