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
    using System.Web.Mvc;
    using ComponentModel;
    using Controllers;
    using MvcTurbine.Blades;

    /// <summary>
    /// Blade for all controller related components.
    /// </summary>
    public class ControllerBlade : Blade, ISupportAutoRegistration {
        /// <summary>
        /// Sets the instance of <see cref="IControllerFactory"/> to use.  If one is not registered,
        /// <see cref="IControllerActivator"/> is used.
        /// </summary>
        public override void Spin(IRotorContext context) {
            // Get the current IServiceLocator
            var locator = GetServiceLocatorFromContext(context);

            // Get the registered controller factory
            var controllerActivator = GetControllerActivator(locator);
            if (controllerActivator != null) return;

            // Register with the runtime -- this is due to the fact that we're using the DefaultControllerFactory uses
            // this new type for creation of the controllers -- we need to inject our own if one is not specified
            using (locator.Batch())
            {
                // Set the default controller factory
                locator.Register<IControllerActivator, TurbineControllerActivator>();
            }
        }

        /// <summary>
        /// Gets the registered <seealso cref="IControllerActivator"/>, if one is not found the default one is used.
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        protected virtual IControllerActivator GetControllerActivator(IServiceLocator locator) {
            try {
                return locator.Resolve<IControllerActivator>();
            }
            catch { return null; }
        }

        public void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList.Add(MvcRegistration.RegisterController());
        }
    }
}