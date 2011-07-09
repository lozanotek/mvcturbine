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
            return Modules;
        }

        /// <summary>
        /// Adds the specified module to the runtime pipeline.
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <returns></returns>
        public virtual HttpModuleRegistry Add<TModule>() where TModule : IHttpModule {
            return Add<TModule>(typeof(TModule).Name);
        }

        /// <summary>
        /// Adds the specified module with the specified name to the runtime pipeline.
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public virtual HttpModuleRegistry Add<TModule>(string moduleName) where TModule : IHttpModule {
            var moduleType = typeof (TModule);
            return Add(moduleType, moduleName);
        }
        
        /// <summary>
        /// Adds the specified module to the runtime pipeline.
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public virtual HttpModuleRegistry Add(Type moduleType) {
            if (moduleType == null) return this;
            if (!moduleType.IsType<IHttpModule>()) return this;

            return Add(moduleType, moduleType.Name);
        }

        /// <summary>
        /// Adds the specified module with the specified name to the runtime pipeline.        
        /// </summary>
        /// <param name="moduleType"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public virtual HttpModuleRegistry Add(Type moduleType, string moduleName) {
            if (moduleType == null) return this;
            if (!moduleType.IsType<IHttpModule>()) return this;

            Modules.Add(new HttpModule { Type = moduleType, Name = moduleName });
            return this;
        }

        /// <summary>
        /// Removes the specified module name from the runtime pipeline.
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public virtual HttpModuleRegistry Remove(string moduleName) {
            Modules.Add(new HttpModule { IsRemoved = true, Name = moduleName });
            return this;
        }

        /// <summary>
        /// Removes the specified module from the runtime pipeline.        
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <returns></returns>
        public virtual HttpModuleRegistry Remove<TModule>() where TModule : IHttpModule {
            return Remove(typeof(TModule).Name);
        }

        /// <summary>
        /// Removes the specified module from the runtime pipeline.        
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public virtual HttpModuleRegistry Remove(Type moduleType)  {
            if (moduleType == null) return this;
            if (!moduleType.IsType<IHttpModule>()) return this;

            Modules.Add(new HttpModule { IsRemoved = true, Name = moduleType.Name });
            return this;
        }
    }
}
