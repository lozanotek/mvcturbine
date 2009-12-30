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
        private static MvcBlade mvcBlade;
        private static RoutingBlade routingBlade;

        /// <summary>
        /// Gets or sets the <see cref="MvcBlade"/> instance to use.
        /// </summary>
        public static MvcBlade Mvc {
            get {
                if (mvcBlade == null) {
                    mvcBlade = new MvcBlade();
                }

                return mvcBlade;
            }

            set { mvcBlade = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="RoutingBlade"/> instance to use.
        /// </summary>
        public static RoutingBlade Routing {
            get {
                if (routingBlade == null) {
                    routingBlade = new RoutingBlade();
                }

                return routingBlade;
            }

            set { routingBlade = value; }
        }

        /// <summary>
        /// Gets the registered core blades in a <see cref="BladeList"/>.
        /// </summary>
        /// <returns></returns>
        public static BladeList GetBlades() {
            return new BladeList { Routing, Mvc};
        }
    }
}