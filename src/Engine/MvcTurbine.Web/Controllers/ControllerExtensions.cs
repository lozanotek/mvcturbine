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
    using System.Web.Mvc;
    using ComponentModel;

    /// <summary>
    /// Extension methods for Controllers.
    /// </summary>
    public static class ControllerExtensions {
        /// <summary>
        /// Gets the current <see cref="ITurbineApplication"/> associated with the MVC application.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <returns>Current <see cref="ITurbineApplication"/> or null if not applicable.</returns>
        internal static ITurbineApplication TurbineApplication(this ControllerBase controller) {
            var httpContext = controller.ControllerContext.HttpContext;
            if(httpContext == null) return null;

            return httpContext.ApplicationInstance as ITurbineApplication; 
        }

        /// <summary>
        /// Gets the current <see cref="IRotorContext"/> associated with the MVC application.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <returns>Current <see cref="IRotorContext"/> or null if not applicable.</returns>
        internal static IRotorContext RotorContext(this ControllerBase controller) {
            var turbineApplication = TurbineApplication(controller);
            return turbineApplication == null ? null : turbineApplication.CurrentContext;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceLocator"/> associated with the MVC application.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <returns>Current <see cref="IServiceLocator"/> or nul if not applicable</returns>
        public static IServiceLocator ServiceLocator(this ControllerBase controller) {
            var turbineApplication = TurbineApplication(controller);
            return turbineApplication == null ? null : turbineApplication.ServiceLocator;
        }

        /// <summary>
        /// Gets the current <see cref="ITurbineApplication"/> associated with the MVC application.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <returns>Current <see cref="ITurbineApplication"/> or null if not applicable.</returns>
        internal static ITurbineApplication TurbineApplication(this ControllerContext controllerContext)
        {
            var httpContext = controllerContext.HttpContext;
            if (httpContext == null) return null;

            return httpContext.ApplicationInstance as ITurbineApplication;
        }

        /// <summary>
        /// Gets the current <see cref="IRotorContext"/> associated with the MVC application.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <returns>Current <see cref="IRotorContext"/> or null if not applicable.</returns>
        internal static IRotorContext RotorContext(this ControllerContext controller)
        {
            var turbineApplication = TurbineApplication(controller);
            return turbineApplication == null ? null : turbineApplication.CurrentContext;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceLocator"/> associated with the MVC application.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <returns>Current <see cref="IServiceLocator"/> or nul if not applicable</returns>
        public static IServiceLocator ServiceLocator(this ControllerContext controller)
        {
            var turbineApplication = TurbineApplication(controller);
            return turbineApplication == null ? null : turbineApplication.ServiceLocator;
        }
    }
}
