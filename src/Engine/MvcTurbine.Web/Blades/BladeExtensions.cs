namespace MvcTurbine.Web.Blades {
    using System.Linq;
    using MvcTurbine.Blades;

    /// <summary>
    /// Helper class for <see cref="IBlade"/> types.
    /// </summary>
    public static class BladeExtensions {
        /// <summary>
        /// Checks whether the specified <see cref="IBlade"/> is a core blade, <see cref="ViewBlade"/> 
        /// or <see cref="RoutingBlade"/>.
        /// </summary>
        /// <param name="blade"></param>
        /// <returns></returns>
        public static bool IsCoreBlade(this IBlade blade) {
            var bladeTypes = CoreBlades.CoreBladeTypes;
            var type = blade.GetType();
            return bladeTypes.Any(bladeType => bladeType.IsAssignableFrom(type));
        }
    }
}
