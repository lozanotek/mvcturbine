namespace MvcTurbine.Web.Blades {
	using System;
	using MvcTurbine.Blades;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using MvcTurbine.ComponentModel;

	/// <summary>
	/// Base class for all blades that need to be core for the engine.s
	/// </summary>
	public abstract class CoreBlade : Blade {
		// Will eventually add logic into this type
	}

	/// <summary>
	/// Class used to keep track of the <see cref="CoreBlade"/> types and 
	/// instantiate them through container.
	/// </summary>
	public static class CoreBlades {
		private static IDictionary<Type, Type> bladeTable = new Dictionary<Type, Type>();

		/// <summary>
		/// Adds the specified <see cref="CoreBlade"/> type to the system.
		/// </summary>
		/// <typeparam name="TBlade"></typeparam>
		internal static void Track<TBlade>() where TBlade: CoreBlade {
			bladeTable[typeof(TBlade)] = typeof(TBlade);
		}

		/// <summary>
		/// Removes the specified <see cref="CoreBlade"/> type from the system.
		/// </summary>
		/// <typeparam name="TBlade"></typeparam>
		internal static void UnTrack<TBlade>() where TBlade : CoreBlade {
			bladeTable.Remove(typeof(TBlade));
		}
		
		/// <summary>
		/// Registers the tracked <see cref="CoreBlade"/> types with the <see cref="IServiceLocator"/>.
		/// </summary>
		/// <param name="locator"></param>
		internal static void RegisterWithServiceLocator(IServiceLocator locator) {
			if (bladeTable.Count == 0) return;

			foreach (var bladeType in bladeTable.Keys) {
				locator.Register(bladeType, bladeType);
			}
		}

		/// <summary>
		/// Gets all the <see cref="CoreBlade"/> types that were registered with the system.
		/// </summary>
		/// <param name="locator"></param>
		/// <returns></returns>
		public static BladeList GetCoreBlades(this IServiceLocator locator) {
			if (bladeTable.Count == 0) return null;

			var bladeList = new BladeList();

			foreach (var bladeType in bladeTable.Values) {
				var blade = locator.Resolve(bladeType) as CoreBlade;
				bladeList.Add(blade);
			}

			return bladeList;
		}
	}
}
