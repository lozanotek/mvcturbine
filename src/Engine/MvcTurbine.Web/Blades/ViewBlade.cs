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

    /// <summary>
    /// Provides all the functionality to wire up <see cref="IViewEngine"/> types for the 
    /// runtime to use.
    /// </summary>
    public class ViewBlade : Blade, ISupportAutoRegistration {
        /// <summary>
        /// Provides the auto-registration of <see cref="IViewEngine"/>.
        /// </summary>
        /// <param name="registrationList"></param>
        public virtual void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList.Add(MvcRegistration.RegisterViewEngine());
        }

        /// <summary>
        /// Executes the setup of all the found <see cref="IViewEngine"/> types.
        /// </summary>
        /// <param name="context"></param>
        public override void Spin(IRotorContext context) {
            SetupViewEngines(context);
        }

        /// <summary>
        /// Initializes the <see cref="ViewEngines.Engines"/> by pulling all associated <seealso cref="IViewEngine"/> instances
        /// in the current application.
        /// </summary>
        /// <param name="context"></param>
        public virtual void SetupViewEngines(IRotorContext context) {
            // Get the current IServiceLocator
            var locator = GetServiceLocatorFromContext(context);

            IList<IViewEngine> viewEngines = GetViewEngines(locator);

            // Clear all ViewEngines
            ViewEngines.Engines.Clear();

            // Add any registered ones
            if (viewEngines != null && viewEngines.Count > 0) {
                viewEngines.ForEach(ViewEngines.Engines.Add);
            }

            // Re-add the WebForms view engine since that's the default one
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
    }
}
