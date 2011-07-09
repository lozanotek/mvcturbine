namespace MvcTurbine.Web {
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Extension method classes
    /// </summary>
    public static class TypeExtensions {
        /// <summary>
        /// Checks to see if the specified type is an MVC filter.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsMvcFilter(this Type currentType) {
            return currentType.IsType<IActionFilter>() ||
                   currentType.IsType<IExceptionFilter>() ||
                   currentType.IsType<IAuthorizationFilter>() ||
                   currentType.IsType<IResultFilter>() ||
                   currentType.IsType<IMvcFilter>();
        }

        /// <summary>
        /// Checks to see if the specified type is a <see cref="IController"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsController(this Type type) {
            return type.IsType<IController>();
        }
    }
}
