namespace MvcTurbine.ComponentModel {
    /// <summary>
    /// Extension methods for <see cref="IServiceLocator"/>.
    /// </summary>
    public static class ServiceLocatorExtensions {
        /// <summary>
        /// Gets the associated container with the current <see cref="IServiceLocator"/> instance.
        /// </summary>
        /// <typeparam name="TContainer"></typeparam>
        /// <param name="locator"></param>
        /// <returns></returns>
        public static TContainer GetUnderlyingContainer<TContainer>(this IServiceLocator locator)
            where TContainer : class {
            if (locator == null) return null;

            var property = locator.GetType().GetProperty("Container");
            if (property == null) return null;

            return property.GetValue(locator, null) as TContainer;
        }
    }
}
