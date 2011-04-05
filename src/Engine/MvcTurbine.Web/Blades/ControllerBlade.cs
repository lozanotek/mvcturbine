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
    /// Provides all the functionality to wire up the specified <see cref="IControllerFactory"/>
    /// for the runtime to use.
    /// </summary>
    public class ControllerBlade : Blade, ISupportAutoRegistration {
        public override void Spin(IRotorContext context) {
            SetupControllerFactory(context);
        }

        /// <summary>
        /// Sets the instance of <see cref="IControllerFactory"/> to use.  If one is not registered,
        /// <see cref="TurbineControllerFactory"/> is used.
        /// </summary>
        public virtual void SetupControllerFactory(IRotorContext context) {
            // Get the current IServiceLocator
            var locator = GetServiceLocatorFromContext(context);

            // Get the registered controller factory
            var controllerFactory = GetControllerFactory(locator);

            // Set the default controller factory
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        /// <summary>
        /// Gets the registered <seealso cref="IControllerFactory"/>, if one is not found the default one is used.
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        protected virtual IControllerFactory GetControllerFactory(IServiceLocator locator) {
            IControllerFactory controllerFactory;

            try {
                controllerFactory = locator.Resolve<IControllerFactory>() ??
                                    new TurbineControllerFactory(locator);
            } catch {
                // Use default factory since one was not specified
                return new TurbineControllerFactory(locator);
            }

            return controllerFactory;
        }

        public void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList.Add(MvcRegistration.RegisterController());
        }
    }
}