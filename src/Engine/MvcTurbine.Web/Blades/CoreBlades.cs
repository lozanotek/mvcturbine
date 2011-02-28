#region License

//
// Author: Javier Lozano <javier@lozanotek.com>
// Copyright (c) 2009-2010, lozanotek, inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

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
        /// Gets the registered core blades in a <see cref="BladeList"/>.
        /// </summary>
        /// <returns></returns>
        public static BladeList GetBlades() {
            return new BladeList { DependencyResolver, Routing, Filters, Controllers, Models, Views };
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