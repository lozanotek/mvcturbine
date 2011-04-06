namespace MvcTurbine.Web.Config
{
    using System;
    using System.Collections.Generic;
    using MvcTurbine.ComponentModel;
    using System.Web.Mvc;

	/// <summary>
	/// Defines the configuration component for runtime elements of the engine.
	/// </summary>
    public sealed class Engine {
        private static IDictionary<Type, Type> engineRegistrations = new Dictionary<Type, Type>();
        private static Engine instance = new Engine();

        /// <summary>
        /// Private default constructor.
        /// </summary>
        private Engine() {
            // Register the default types for the system to use

            RotorContext<RotorContext>()
            .AutoRegistrator<DefaultAutoRegistrator>()
            .AssemblyLoader<DefaultBinAssemblyLoader>();
        }

        /// <summary>
        /// Accesses the initialization points for the engine components.
        /// </summary>
        public static Engine Initialize { get { return instance; } }

        /// <summary>
        /// Registers the <see cref="IRotorContext"/> for the engine to use. If none is specified, <see cref="RotorContext"/> is used.
        /// </summary>
        /// <typeparam name="TContext">A type that implements <see cref="IRotorContext"/>.</typeparam>
        /// <returns></returns>
        public Engine RotorContext<TContext>() where TContext : IRotorContext {
            engineRegistrations[typeof(IRotorContext)] = typeof(TContext);
            return this;
        }

        /// <summary>
        /// Registers the <see cref="IBinAssemblyLoader"/> for the engine to use.  If none is specified, <see cref="DefaultBinAssemblyLoader"/> is used.
        /// </summary>
        /// <typeparam name="TLoader">A type that implements <see cref="IBinAssemblyLoader"/>.</typeparam>
        /// <returns></returns>
        public Engine AssemblyLoader<TLoader>() where TLoader : IBinAssemblyLoader {
            engineRegistrations[typeof(IBinAssemblyLoader)] = typeof(TLoader);
            return this;
        }

        /// <summary>
        /// Registers the <see cref="IAutoRegistrator"/> for the engine to use.  If none is specified, <see cref="DefaultAutoRegistrator"/> is sued.
        /// </summary>
        /// <typeparam name="TRegistrator">A type that implements</typeparam>
        /// <returns></returns>
        public Engine AutoRegistrator<TRegistrator>() where TRegistrator : IAutoRegistrator {
            engineRegistrations[typeof(IAutoRegistrator)] = typeof(TRegistrator);
            return this;
        }

        /// <summary>
        /// Configures the specified types for the engine with the <see cref="IServiceLocator"/>.        
        /// </summary>
        /// <param name="locator">The <see cref="IServiceLocator"/> to use.</param>
        internal void ConfigureWithServiceLocator(IServiceLocator locator) {
            // Start the reg
            using (locator.Batch()) {
                foreach (var item in engineRegistrations) {
                    locator.Register(item.Key, item.Value);
                }
            }
        }
    }
}
