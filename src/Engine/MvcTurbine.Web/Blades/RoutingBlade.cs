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
    using System.Collections.Generic;
    using System.Web.Routing;
    using ComponentModel;
    using MvcTurbine.Blades;
    using Routing;

    /// <summary>
    /// Default <see cref="IBlade"/> that supports all ASP.NET Url routing components.    
    /// </summary>
    public class RoutingBlade : Blade, ISupportAutoRegistration {
        
        /// <summary>
        /// Provides the auto-registration of MVC related components (controllers, view engines, filters, etc).
        /// </summary>
        /// <param name="registrationList"></param>
        public void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList.Add(Registration.Simple<IRouteRegistrator>());
        }

        /// <summary>
        /// Performs the main processing for routing.
        /// </summary>
        /// <param name="context"></param>
        public override void Spin(IRotorContext context) {
            IServiceLocator locator = GetServiceLocatorFromContext(context);
            ProcessRouteConfigurators(locator);
        }

        /// <summary>
        /// Iterates through all the registered <see cref="IRouteRegistrator"/> instances
        /// and wires them up with <see cref="RouteTable.Routes"/>.
        /// </summary>
        public virtual void ProcessRouteConfigurators(IServiceLocator locator) {
            var routeList = GetRouteRegistrations(locator);
            if (routeList == null) return;

            foreach (var routeConfigurator in routeList) {
                routeConfigurator.Register(RouteTable.Routes);
            }
        }

        /// <summary>
        /// Gets the <see cref="IRouteRegistrator"/> registered with the system.
        /// </summary>
        /// <param name="locator">Instance of <see cref="IServiceLocator"/> to use.</param>
        /// <returns>List of <see cref="IRouteRegistrator"/>, null otherwise.</returns>
        protected virtual IList<IRouteRegistrator> GetRouteRegistrations(IServiceLocator locator) {
            try {
                return locator.ResolveServices<IRouteRegistrator>();
            } catch {
                return null;
            }
        }
    }
}
