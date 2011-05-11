namespace MvcTurbine.Web.Config {
	using System;
	using MvcTurbine.Web.Blades;

	/// <summary>
	/// Extension class for <see cref="Engine"/>.
	/// </summary>
	public static class EngineBladeExt {
		/// <summary>
		/// Disables the auto-registration of <see cref="IHttpModule"/> types.
		/// </summary>
		/// <param name="engine"></param>
		/// <returns></returns>
		public static Engine DisableHttpModuleRegistration(this Engine engine) {
			engine.RemoveCoreBlade<HttpModuleBlade>();
			return engine;
		}

		/// <summary>
		/// Registers all built-in <see cref="CoreBlade"/> types that the engine currently supports.
		/// </summary>
		/// <param name="engine"></param>
		/// <returns></returns>
		internal static Engine RegisterBuiltInCoreBlades(this Engine engine) {
			engine.WithCoreBlade<DependencyResolverBlade>()
				.WithCoreBlade<RoutingBlade>()
				.WithCoreBlade<FilterBlade>()
				.WithCoreBlade<ControllerBlade>()
				.WithCoreBlade<ModelBinderBlade>()
				.WithCoreBlade<ViewBlade>()
				.WithCoreBlade<InferredActionBlade>()
				.WithCoreBlade<EmbeddedViewBlade>()
				.WithCoreBlade<MetadataProviderBlade>()
				.WithCoreBlade<HttpModuleBlade>();

			return engine;
		}

		/// <summary>
		/// Adds the specified <see cref="CoreBlade"/> with the engine.
		/// </summary>
		/// <typeparam name="TBlade"></typeparam>
		/// <param name="engine"></param>
		/// <returns></returns>
		public static Engine WithCoreBlade<TBlade>(this Engine engine)
			where TBlade : CoreBlade {
			engine.EngineRegistration<TBlade, TBlade>();
			return engine;
		}

		/// <summary>
		/// Removes the specified <see cref="CoreBlade"/> from the engine.
		/// </summary>
		/// <typeparam name="TBlade"></typeparam>
		/// <param name="engine"></param>
		/// <returns></returns>
		public static Engine RemoveCoreBlade<TBlade>(this Engine engine)
			where TBlade : CoreBlade {
			engine.RemoveRegistration<TBlade>();
			return engine;
		}
	}

}
