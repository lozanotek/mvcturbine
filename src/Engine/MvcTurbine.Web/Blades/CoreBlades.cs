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
    using MvcTurbine.Blades;

    /// <summary>
    /// Static gateway for the core blades of MVC Turbine.
    /// </summary>
    public static class CoreBlades {
        private static Blade viewBlade;
        private static Blade routingBlade;
        private static Blade controllerBlade;
        private static Blade filterBlade;
        private static Blade binderBlade;

        /// <summary>
        /// Gets or sets the blade that handles the model binder registration.
        /// </summary>
        public static Blade Model {
            get { return binderBlade ?? (binderBlade = new ModelBinderBlade()); }
            set { binderBlade = value; }
        }

        /// <summary>
        /// Gets or sets the blade that handles the filter registration.
        /// </summary>
        public static Blade Filter {
            get { return filterBlade ?? (filterBlade = new FilterBlade()); }
            set { filterBlade = value; }
        }

        /// <summary>
        /// Gets or sets the blade that handles the controller registration.
        /// </summary>
        public static Blade Controller {
            get { return controllerBlade ?? (controllerBlade = new ControllerBlade()); }
            set { controllerBlade = value; }
        }

        /// <summary>
        /// Gets or sets the blade that handles the view engine registration.
        /// </summary>
        public static Blade View {
            get { return viewBlade ?? (viewBlade = new ViewBlade()); }
            set { viewBlade = value; }
        }

        /// <summary>
        /// Gets or sets the blade that handles the routing registration.
        /// </summary>
        public static Blade Routing {
            get { return routingBlade ?? (routingBlade = new RoutingBlade()); }
            set { routingBlade = value; }
        }

        /// <summary>
        /// Gets the registered core blades in a <see cref="BladeList"/>.
        /// </summary>
        /// <returns></returns>
        public static BladeList GetBlades() {
            return new BladeList { Routing, Controller, Model, View, Filter };
        }
    }
}