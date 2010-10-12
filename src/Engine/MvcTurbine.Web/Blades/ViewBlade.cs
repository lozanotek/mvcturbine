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
    using System.Web.Mvc;
    using ComponentModel;
    using Controllers;
    using MvcTurbine.Blades;

    ///<summary>
    /// Blade for all View related components
    ///</summary>
    public class ViewBlade : Blade, ISupportAutoRegistration {
        /// <summary>
        /// Initializes the <see cref="ViewEngines.Engines"/> by pulling all associated <seealso cref="IViewEngine"/> instances
        /// in the current application.
        /// </summary>
        /// <param name="context"></param>
        public override void Spin(IRotorContext context) {
            // Get the current IServiceLocator
            var locator = GetServiceLocatorFromContext(context);

            // Clear all ViewEngines
            ViewEngines.Engines.Clear();

            var viewEngines = GetViewEngines(locator);

            // Add any registered ones
            if (viewEngines != null && viewEngines.Count > 0) {
                foreach (var viewEngine in viewEngines) {
                    ViewEngines.Engines.Add(viewEngine);
                }
            }

            // Re-add the WebForms view engine since that's the default one
            ViewEngines.Engines.Add(new RazorViewEngine());
            ViewEngines.Engines.Add(new WebFormViewEngine());
        }

        /// <summary>
        /// Gets the <see cref="IViewEngine"/> registered with the system.
        /// </summary>
        /// <param name="locator">Instance of <see cref="IServiceLocator"/> to use.</param>
        /// <returns>List of <see cref="IViewEngine"/>, null otherwise.</returns>
        protected virtual IList<IViewEngine> GetViewEngines(IServiceLocator locator) {
            try {
                return locator.ResolveServices<IViewEngine>();
            } catch {
                return null;
            }
        }

        public void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList.Add(MvcRegistration.RegisterViewEngine());
        }
    }
}