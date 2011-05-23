namespace Okonau.Persistence {
    using System;
    using System.Web;
    using NHibernate;
    using NHibernate.Context;

    /// <summary>
    /// Defines the getting and setting of <see cref="ISession"/> for the request lifetime.
    /// </summary>
    public class NHibernateSessionModule : IHttpModule {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="database"></param>
        public NHibernateSessionModule(IOkonauPersistence database) {
            Database = database;
        }

        /// <summary>
        /// Gets the specified <see cref="IOkonauPersistence"/> to use.
        /// </summary>
        public IOkonauPersistence Database { get; private set; }

        public void Init(HttpApplication context) {
            context.BeginRequest += BeginRequest;
            context.EndRequest += EndRequest;
        }

        public void Dispose() {
        }

        public virtual void BeginRequest(object sender, EventArgs e) {
            var application = (HttpApplication) sender;
            HttpContext context = application.Context;

            BindSession(context);
        }

        protected virtual void BindSession(HttpContext context) {
            ISession session = Database.OpenSession();

            // Tell NH session context to use it
            ManagedWebSessionContext.Bind(context, session);
        }

        public virtual void EndRequest(object sender, EventArgs e) {
            var application = (HttpApplication) sender;
            HttpContext context = application.Context;

            UnbindSession(context);
        }

        protected virtual void UnbindSession(HttpContext context) {
            // Get the default NH session factory
            ISessionFactory factory = Database.GetSessionFactory();
            ISession session = ManagedWebSessionContext.Unbind(context, factory);

            try {
                // Give it to NH so it can pull the right session

                if (session == null) return;

                session.Flush();
                session.Close();
            }
            catch {
                // No need to handle this for this piece.
            }
        }
    }
}