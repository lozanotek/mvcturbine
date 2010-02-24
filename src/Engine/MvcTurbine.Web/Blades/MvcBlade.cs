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
    using Models;
    using MvcTurbine.Blades;

    /// <summary>
    /// Default <see cref="IBlade"/> that supports all ASP.NET MVC components.
    /// </summary>
    public class MvcBlade : Blade, ISupportAutoRegistration {
        
        /// <summary>
        /// Provides the auto-registration of MVC related components (controllers, view engines, filters, etc).
        /// </summary>
        /// <param name="registrationList"></param>
        public virtual void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList
                .Add(MvcRegistration.RegisterController())
                .Add(MvcRegistration.RegisterViewEngine())
                .Add(MvcRegistration.RegisterFilter<IActionFilter>())
                .Add(MvcRegistration.RegisterFilter<IResultFilter>())
                .Add(MvcRegistration.RegisterFilter<IAuthorizationFilter>())
                .Add(MvcRegistration.RegisterFilter<IExceptionFilter>())
                .Add(MvcRegistration.RegisterBinder());
        }

        /// <summary>
        /// Executes the setup of controller factories, view engines and model binders.
        /// </summary>
        /// <param name="context"></param>
        public override void Spin(IRotorContext context) {
            SetupControllerFactory(context);
            SetupViewEngines(context);
            SetupModelBinders(context);
        }

        ///<summary>
        /// Changes the <see cref="ModelBinderDictionary.DefaultBinder"/> instance to be <see cref="TurbineModelBinder"/>.
        ///</summary>
        ///<param name="context">Current <see cref="IRotorContext"/> performing the execution.</param>
        public virtual void SetupModelBinders(IRotorContext context) {
            // Get the current IServiceLocator
            var serviceLocator = GetServiceLocatorFromContext(context);

            // Set the default IModelBinder to use for all requests
            ModelBinders.Binders.DefaultBinder = new TurbineModelBinder(serviceLocator);
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
