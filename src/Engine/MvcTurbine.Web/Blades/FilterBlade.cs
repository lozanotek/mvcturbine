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
    using System.Web.Mvc;
    using ComponentModel;
    using Filters;
    using MvcTurbine.Blades;

    /// <summary>
    /// Default <see cref="IBlade"/> that supports all ASP.NET MVC components.
    /// </summary>
    public class FilterBlade : Blade, ISupportAutoRegistration {

        /// <summary>
        /// Provides the auto-registration of MVC related components (controllers, view engines, filters, etc).
        /// </summary>
        /// <param name="registrationList"></param>
        public virtual void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList
                .Add(Registration.Simple<IFilterProvider>())
                .Add(Registration.Simple<IFilterRegistry>());
        }

        ///<summary>
        /// Sets up the <see cref="IFilterProvider"/>s that have been registered with the system. Also, injects the one from
        /// MVC Turbine.
        ///</summary>
        ///<param name="context">Current <see cref="IRotorContext"/> performing the execution.</param>
        public override void Spin(IRotorContext context) {
            // Get the current IServiceLocator
            var serviceLocator = GetServiceLocatorFromContext(context);

            SetupFilterProviders(serviceLocator);
            SetupFilterRegistries(serviceLocator);
        }

        public virtual void SetupFilterRegistries(IServiceLocator serviceLocator) {
            var filterRegistries = GetFilterRegistries(serviceLocator);
            if (filterRegistries == null) return;

            var filterList = new List<FilterReg>();
            var typeList = new List<Type>();
            foreach (var filterRegistry in filterRegistries) {
                var registrations = filterRegistry.GetFilterRegistrations();

                using (serviceLocator.Batch()) {
                    foreach (var registration in registrations) {
                        var filterType = registration.Filter;

                        // Prevent double registration of the same filter
                        if(typeList.Contains(filterType)) continue;

                        serviceLocator.Register(filterType, filterType);
                        typeList.Add(filterType);
                    }
                }

                filterList.AddRange(registrations);
            }

            typeList.Clear();
            FilterProviders.Providers.Add(new FilterRegistryProvider(serviceLocator, filterList));
        }

        public virtual void SetupFilterProviders(IServiceLocator serviceLocator) {
            // Clear out what's there by default
            FilterProviders.Providers.Clear();

            FilterProviders.Providers.Add(GlobalFilters.Filters);
            FilterProviders.Providers.Add(new ControllerInstanceFilterProvider());
            FilterProviders.Providers.Add(new InjectableAttributeFilterProvider(serviceLocator));

            var filterProviders = GetFilterProviders(serviceLocator);
            if (filterProviders == null || filterProviders.Count == 0)
                return;

            foreach (var filterProvider in filterProviders) {
                FilterProviders.Providers.Add(filterProvider);
            }
        }

        protected virtual IList<IFilterRegistry> GetFilterRegistries(IServiceLocator locator) {
            try {
                return locator.ResolveServices<IFilterRegistry>();
            } catch {
                return null;
            }
        }

        protected virtual IList<IFilterProvider> GetFilterProviders(IServiceLocator locator) {
            try {
                return locator.ResolveServices<IFilterProvider>();
            } catch {
                return null;
            }
        }
    }
}
