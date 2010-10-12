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

namespace MvcTurbine.Web.Controllers {
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using ComponentModel;

    /// <summary>
    /// Activator for the MVC3 runtime to spin up controllers and the registered <see cref="IActionInvoker"/>.
    /// </summary>
    public class TurbineControllerActivator : IControllerActivator {
        private static IActionInvoker actionInvoker;
        private static readonly object _lock = new object();

        public TurbineControllerActivator(IServiceLocator serviceLocator) {
            if (serviceLocator == null) {
                throw new ArgumentNullException("serviceLocator");
            }

            ServiceLocator = serviceLocator;
        }

        /// <summary>
        /// Gets the current instance of <see cref="IServiceLocator"/> for the factory.
        /// </summary>
        public IServiceLocator ServiceLocator { get; private set; }

        public IController Create(RequestContext requestContext, Type controllerType) {
            var instance = ServiceLocator.Resolve<IController>(controllerType);
            var controller = instance as Controller;

            // If you inherit from controller, implement this fine work around
            if (controller != null) {
                controller.ActionInvoker = GetActionInvoker();
            }

            return controller;
        }

        /// <summary>
        /// Gets the registered <see cref="IActionInvoker"/> within the system.
        /// </summary>
        /// <returns></returns>
        protected virtual IActionInvoker GetActionInvoker() {
            if (actionInvoker == null) {
                lock (_lock) {
                    if (actionInvoker == null) {
                        try {
                            actionInvoker = ServiceLocator.Resolve<IActionInvoker>();
                        } catch (ServiceResolutionException) {
                            actionInvoker = new TurbineActionInvoker(ServiceLocator);
                        }
                    }
                }
            }

            return actionInvoker;
        }
    }
}