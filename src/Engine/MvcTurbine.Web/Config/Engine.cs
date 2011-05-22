namespace MvcTurbine.Web.Config
{
	using System;
	using System.Collections.Generic;
	using ComponentModel;
	using Controllers;
	using Views;
	using MvcTurbine.Web.Blades;

	/// <summary>
	/// Defines the configuration component for runtime elements of the engine.
	/// </summary>
	public sealed class Engine {
		private static readonly IDictionary<Type, Type> engineRegistrations = new Dictionary<Type, Type>();
		private static readonly Engine instance = new Engine();

		/// <summary>
		/// Private default constructor.
		/// </summary>
		private Engine() {
			InitliazeDefaults();
		}

		/// <summary>
		/// Sets the defaults for the engine to use.
		/// </summary>
		private void InitliazeDefaults() {
			// Register the default types for the system to use

			RotorContext<RotorContext>()
			.AutoRegistrator<DefaultAutoRegistrator>()
			.AssemblyLoader<DefaultBinAssemblyLoader>()
			.ControllerFactory<TurbineControllerFactory>()
			.ControllerActivator<TurbineControllerActivator>()
			.DependencyResolver<TurbineDependencyResolver>()
			.ActionInvoker<TurbineActionInvoker>()
			.EmbeddedViewResolve<EmbeddedViewResolver>()
			.RegisterBuiltInCoreBlades();
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
			EngineRegistration<IRotorContext, TContext>();
			return this;
		}

		/// <summary>
		/// Registers the <see cref="IBinAssemblyLoader"/> for the engine to use.  If none is specified, <see cref="DefaultBinAssemblyLoader"/> is used.
		/// </summary>
		/// <typeparam name="TLoader">A type that implements <see cref="IBinAssemblyLoader"/>.</typeparam>
		/// <returns></returns>
		public Engine AssemblyLoader<TLoader>() where TLoader : IBinAssemblyLoader {
			EngineRegistration<IBinAssemblyLoader, TLoader>();
			return this;
		}

		/// <summary>
		/// Registers the <see cref="IAutoRegistrator"/> for the engine to use.  If none is specified, <see cref="DefaultAutoRegistrator"/> is sued.
		/// </summary>
		/// <typeparam name="TRegistrator">A type that implements</typeparam>
		/// <returns></returns>
		public Engine AutoRegistrator<TRegistrator>() where TRegistrator : IAutoRegistrator {
			EngineRegistration<IAutoRegistrator, TRegistrator>();
			return this;
		}

		/// <summary>
		/// Configures the specified types for the engine with the <see cref="IServiceLocator"/>.        
		/// </summary>
		/// <param name="locator">The <see cref="IServiceLocator"/> to use.</param>
		internal void ConfigureWithServiceLocator(IServiceLocator locator) {
			if (locator == null) return;

			// Start the reg
			using (locator.Batch()) {
				// Add the IServiceLocator instance to itself so if any types later on need it,
				// they can have it injected into them.
				//
				// Use the factory method approach to prevent the stackoverflow error
				locator.Register(() => locator);

				// To provide DI support for IServiceReleaser
				if (locator is IServiceReleaser) {
					locator.Register(() => locator as IServiceReleaser);
				}
				else {
					locator.Register<IServiceReleaser, EmptyServiceReleaser>();
				}

			    // To provide DI support for IServiceInjector
				if (locator is IServiceInjector) {
					locator.Register(() => locator as IServiceInjector);
				}
				else {
					locator.Register<IServiceInjector, EmptyServiceInjector>();
				}

			    foreach (var item in engineRegistrations) {
					locator.Register(item.Key, item.Value);
				}

				// Register these pieces with the engine
				CoreBlades.RegisterWithServiceLocator(locator);

				// No longer needed
				engineRegistrations.Clear();
			}
		}

		/// <summary>
		/// Adds registrations to the internal engine types.
		/// </summary>
		/// <typeparam name="TService"></typeparam>
		/// <typeparam name="TImpl"></typeparam>
		internal void EngineRegistration<TService,TImpl>() {
			engineRegistrations[typeof(TService)] = typeof(TImpl);
		}

        /// <summary>
        /// Removes the specified registration from the internal engine types.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        internal void RemoveRegistration<TService>() {
            var key = typeof (TService);

            if (!engineRegistrations.ContainsKey(key)) return;
            engineRegistrations.Remove(key);
        }
	}
}
