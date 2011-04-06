namespace MvcTurbine.Routing {
    using System.Web.Routing;

    /// <summary>
    /// Provides a simple way to register routes within your application.
    /// </summary>
    public interface IRouteRegistrator {
        /// <summary>
        /// Registers routes within <see cref="RouteCollection"/> for the application.
        /// </summary>
        /// <param name="routes">The <see cref="RouteCollection"/> from the <see cref="RouteTable.Routes"/>.</param>
        void Register(RouteCollection routes);
    }
}