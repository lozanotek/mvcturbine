namespace MvcTurbine.Web.Filters {
    using System;

    /// <summary>
    /// Provides a simple helper to wrap a generic based action delegate to an object instance.
    /// </summary>
    public static class FilterRegistryHelper {
        public static Action<object> WrapInitializer<TFilter>(Action<TFilter> initializer) {
            return (initializer == null) ? (Action<object>)null : instance => initializer((TFilter)instance);
        }
    }
}
