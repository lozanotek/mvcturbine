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
    /// Controller Factory class for instantiating controllers using the Windsor IoC container.
    /// </summary>
    public class TurbineControllerFactory : DefaultControllerFactory {
        private static IActionInvoker actionInvoker;
        private static readonly object _lock = new object();

        /// <summary>
        /// Creates a new instance of the <see cref="TurbineControllerFactory"/> class.
        /// </summary>
        /// <param name="serviceLocator">The <see cref="IServiceLocator"/> to use when 
        /// creating controllers.</param>
        public TurbineControllerFactory(IServiceLocator serviceLocator) {
            if (serviceLocator == null) {
                throw new ArgumentNullException("serviceLocator");
            }

            ServiceLocator = serviceLocator;
        }

        /// <summary>
        /// Gets the current instance of <see cref="IServiceLocator"/> for the factory.
        /// </summary>
        public IServiceLocator ServiceLocator { get; private set; }

        /// <summary>
        /// Provides the implementation of <see cref="IController"/> from the current
        /// <see cref="IServiceLocator"/>.
        /// </summary>
        /// <param name="requestContext">Request context for the current request.</param>
        /// <param name="controllerType">Type of controller to search for.</param>
        /// <returns>An instance of <see cref="IController"/> from the container.</returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
            if (controllerType == null) {
                // this will make sure that the MVC framework throws the corresponding 
                // exception for a non-registered controller
                return base.GetControllerInstance(requestContext, controllerType);
            }

            var instance = ServiceLocator.Resolve<IController>(controllerType);
            var controller = instance as Controller;

            // If you inherit from controller, implement this fine work around
            if (controller != null) {
                controller.ActionInvoker = GetActionInvoker();
            }

            return controller;
        }

        /// <summary>
        /// Releases the controller by giving it back to <see cref="IServiceLocator"/>.
        /// </summary>
        /// <param name="controller">Controller to dispose.</param>
        public override void ReleaseController(IController controller) {
            if (controller == null) return;

            var disposable = controller as IDisposable;

            if (disposable != null) {
                disposable.Dispose();
            }

            ServiceLocator.Release(controller);
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