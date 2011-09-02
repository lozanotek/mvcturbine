namespace MvcTurbine.Web.Views {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// Base implementation of <see cref="IViewEngineProvider"/> that provides a fluent interface for registration.
    /// </summary>
    public abstract class ViewEngineRegistry : IViewEngineProvider {
        /// <summary>
        /// Default protected constructor
        /// </summary>
        protected ViewEngineRegistry() {
            Engines = new List<ViewEngine>();
        }

        /// <summary>
        /// Gets the internal list of <see cref="ViewEngine"/> that's being processed.
        /// </summary>
        protected IList<ViewEngine> Engines { get; set; }

        /// <summary>
        /// Adds the specified module to the runtime pipeline.
        /// </summary>
        /// <typeparam name="TEngine"></typeparam>
        /// <returns></returns>
        public virtual ViewEngineRegistry Add<TEngine>() where TEngine : IViewEngine {
            return Add<TEngine>(typeof(TEngine).Name);
        }

        /// <summary>
        /// Adds the specified module with the specified name to the runtime pipeline.
        /// </summary>
        /// <typeparam name="TEngine"></typeparam>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public virtual ViewEngineRegistry Add<TEngine>(string moduleName) where TEngine : IViewEngine {
            var moduleType = typeof(TEngine);
            return Add(moduleType);
        }

        /// <summary>
        /// Adds the specified module to the runtime pipeline.
        /// </summary>
        /// <param name="engineType"></param>
        /// <returns></returns>
        public virtual ViewEngineRegistry Add(Type engineType) {
            if (engineType == null) return this;
            if (!engineType.IsType<IViewEngine>()) return this;

            Engines.Add(new ViewEngine { Type = engineType, Name = engineType.Name });
            return this;
        }

        /// <summary>
        /// Removes the specified module from the runtime pipeline.        
        /// </summary>
        /// <typeparam name="TEngine"></typeparam>
        /// <returns></returns>
        public virtual ViewEngineRegistry Remove<TEngine>() where TEngine : IViewEngine {
            var engineType = typeof (TEngine);
            if (!engineType.IsType<IViewEngine>()) return this;

            Engines.Add(new ViewEngine { IsRemoved = true, Name = engineType.Name });
            return this;
        }

        /// <summary>
        /// See <see cref="IViewEngineProvider.GetViewEngineRegistrations"/>
        /// </summary>
        /// <returns></returns>
        public virtual IList<ViewEngine> GetViewEngineRegistrations() {
            return Engines;
        }
    }
}
