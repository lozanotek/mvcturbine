#region License

//
// Author: Javier Lozano <javier@lozanotek.com>
// Copyright (c) 2009-2010, lozanotek, inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

namespace MvcTurbine.Mvc2 {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;
    using MvcTurbine.Blades;
    using MvcTurbine.ComponentModel;

    public class AreaBlade : Blade, ISupportAutoRegistration {
        public virtual void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList.Add(Registration.Simple<AreaRegistration>());
        }

        public override void Spin(IRotorContext context) {
            var serviceLocator = GetServiceLocatorFromContext(context);
            var areaList = GetRegisteredAreas(serviceLocator);

            if (areaList == null || areaList.Count == 0) return;

            var areaRoutes = new RouteCollection();
            foreach (AreaRegistration registration in areaList) {
                var registrationContext = new AreaRegistrationContext(registration.AreaName, areaRoutes);
                var areaNamespace = registration.GetType().Namespace;

                if (areaNamespace != null) {
                    registrationContext.Namespaces.Add(areaNamespace + ".*");
                }

                registration.RegisterArea(registrationContext);
            }

            ReOrderRoutingTable(areaRoutes);
        }

        protected virtual void ReOrderRoutingTable(RouteCollection areaRoutes) {
            var existingRoutes = new RouteBase[RouteTable.Routes.Count];
            RouteTable.Routes.CopyTo(existingRoutes, 0);

            var aggregateList = new List<RouteBase>();
            aggregateList.AddRange(areaRoutes);
            aggregateList.AddRange(existingRoutes);

            RouteTable.Routes.Clear();

            foreach (var route in aggregateList)
            {
                RouteTable.Routes.Add(route);
            }
        }

        public virtual IList<AreaRegistration> GetRegisteredAreas(IServiceLocator locator) {
            try {
                return locator.ResolveServices<AreaRegistration>();
            }
            catch {
                return null;
            }
        }
    }
}
