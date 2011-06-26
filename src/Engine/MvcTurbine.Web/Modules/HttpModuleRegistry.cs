namespace MvcTurbine.Web.Modules {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    ///<summary>
    /// Base class for all <see cref="IHttpModule"/> registrations.
    ///</summary>
    public abstract class HttpModuleRegistry : IHttpModuleRegistry {
        protected HttpModuleRegistry() {
            Modules = new List<HttpModule>();
        }

        protected IList<HttpModule> Modules { get; set; }

        /// <summary>
        /// Gets all the registered <see cref="IHttpModule"/> with the runtime.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<HttpModule> GetModuleRegistrations() {
            return Modules == null ? null : Modules.Distinct();
        }

        public virtual HttpModuleRegistry Add<TModule>() where TModule : IHttpModule {
            return Add(typeof (TModule));
        }

        public virtual HttpModuleRegistry Add(Type moduleType) {
            if (moduleType == null) return this;
            if (!moduleType.IsType<IHttpModule>()) return this;

            Modules.Add(new HttpModule {Type = moduleType});
            return this;
        }

        public virtual HttpModuleRegistry Remove<TModule>() where TModule : IHttpModule {
            return Remove(typeof(TModule));
        }

        public virtual HttpModuleRegistry Remove(Type moduleType)  {
            if (moduleType == null) return this;
            if (!moduleType.IsType<IHttpModule>()) return this;

            Modules.Add(new HttpModule { Type = moduleType, IsRemoved = true });
            return this;
        }
    }
}
