namespace MvcTurbine.Web.Blades {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MvcTurbine.Blades;

    /// <summary>
    /// Static gateway for the core blades of MVC Turbine.
    /// </summary>
    public static class CoreBlades {
        private static FilterBlade filterBlade;
        private static RoutingBlade routingBlade;
        private static DependencyResolverBlade dependencyResolverBlade;
        private static ControllerBlade controllerBlade;
        private static ModelBinderBlade modelBlade;
        private static ViewBlade viewBlade;
        private static InferredActionBlade actionBlade;

        /// <summary>
        /// Gets or sets the <see cref="ControllerBlade"/> instance to use.
        /// </summary>
        public static ControllerBlade Controllers {
            get { return controllerBlade ?? (controllerBlade = new ControllerBlade()); }
            set { controllerBlade = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="ViewBlade"/> instance to use.
        /// </summary>
        public static ViewBlade Views {
            get { return viewBlade ?? (viewBlade = new ViewBlade()); }
            set { viewBlade = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="ModelBinderBlade"/> instance to use.
        /// </summary>
        public static ModelBinderBlade Models {
            get { return modelBlade ?? (modelBlade = new ModelBinderBlade()); }
            set { modelBlade = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="FilterBlade"/> instance to use.
        /// </summary>
        public static FilterBlade Filters {
            get { return filterBlade ?? (filterBlade = new FilterBlade()); }
            set { filterBlade = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="RoutingBlade"/> instance to use.
        /// </summary>
        public static RoutingBlade Routing {
            get { return routingBlade ?? (routingBlade = new RoutingBlade()); }
            set { routingBlade = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="DependencyResolverBlade"/> instance to use.
        /// </summary>
        public static DependencyResolverBlade DependencyResolver {
            get { return dependencyResolverBlade ?? (dependencyResolverBlade = new DependencyResolverBlade()); }
            set { dependencyResolverBlade = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="InferredActionBlade"/> instance to use.
        /// </summary>
        public static InferredActionBlade InferredAction {
            get { return actionBlade ?? (new InferredActionBlade()); }
            set { actionBlade = value; }
        }

        /// <summary>
        /// Gets the registered core blades in a <see cref="BladeList"/>.
        /// </summary>
        /// <returns></returns>
        public static BladeList GetBlades() {
            return new BladeList { DependencyResolver, Routing, Filters, Controllers, Models, Views, InferredAction };
        }

        internal static IList<Type> CoreBladeTypes {
            get {
                var coreType = typeof(CoreBlades);
                var properties = coreType.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

                return properties.Select(prop => prop.PropertyType).ToList();
            }
        }
    }
}