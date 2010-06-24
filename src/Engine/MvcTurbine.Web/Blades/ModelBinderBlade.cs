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
    using Models;
    using MvcTurbine.Blades;

    /// <summary>
    /// Provides all the functionality to wire up all the <see cref="IModelBinder"/> types
    /// for the runtime to use.
    /// </summary>
    public class ModelBinderBlade : Blade, ISupportAutoRegistration {
        private static IBinderRegistrationManager binderManager;
        private static readonly object _lock = new object();

        public override void Spin(IRotorContext context) {
            SetupModelBinders(context);
        }

        /// <summary>
        /// Sets up registrations for <see cref="IModelBinder"/> types.
        /// </summary>
        /// <param name="registrationList"></param>
        public void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList
                .Add(MvcRegistration.RegisterBinder())
                .Add(Registration.Simple<ModelBinderRegistry>());
        }

        ///<summary>
        /// Changes the <see cref="ModelBinderDictionary.DefaultBinder"/> instance to be <see cref="TurbineModelBinder"/>.
        ///</summary>
        ///<param name="context">Current <see cref="IRotorContext"/> performing the execution.</param>
        public virtual void SetupModelBinders(IRotorContext context) {
            // Get the current IServiceLocator
            var serviceLocator = GetServiceLocatorFromContext(context);

            // Get the current IBinderRegistrationManager
            var manager = GetRegistrationManager(serviceLocator);

            // Set the default IModelBinder to use for all requests
            ModelBinders.Binders.DefaultBinder = new TurbineModelBinder(serviceLocator, manager);
        }

        /// <summary>
        /// Gets the registered <see cref="IBinderRegistrationManager"/>, if null <see cref="DefaultBinderRegistrationManager"/> is used.
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        protected virtual IBinderRegistrationManager GetRegistrationManager(IServiceLocator locator) {
            if (binderManager == null) {
                lock (_lock) {
                    if (binderManager == null) {
                        try {
                            binderManager = locator.Resolve<IBinderRegistrationManager>();
                        } catch {
                            binderManager = new DefaultBinderRegistrationManager(locator);
                        }
                    }
                }
            }

            return binderManager;
        }
    }
}