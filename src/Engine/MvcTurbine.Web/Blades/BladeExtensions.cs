namespace MvcTurbine.Web.Blades {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MvcTurbine.Blades;

    /// <summary>
    /// Helper class for <see cref="IBlade"/> types.
    /// </summary>
    public static class BladeExtensions {
        private static readonly IList<Type> bladeTypes = new List<Type> {
            typeof (ViewBlade),
            typeof (FilterBlade),
            typeof (ControllerBlade),
            typeof (ModelBinderBlade),
            typeof (DependencyResolverBlade)
        };

        /// <summary>
        /// Checks whether the specified <see cref="IBlade"/> is a core blade, <see cref="ViewBlade"/> 
        /// or <see cref="RoutingBlade"/>.
        /// </summary>
        /// <param name="blade"></param>
        /// <returns></returns>
        public static bool IsCoreBlade(this IBlade blade) {
            var type = blade.GetType();
            return bladeTypes.Any(bladeType => bladeType.IsAssignableFrom(type));
        }
    }
}
