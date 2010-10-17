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

namespace MvcTurbine.Web.Views {
    using System.Web;
    using System.Web.Mvc;
    using ComponentModel;

    ///<summary>
    /// Extensions for <see cref="ViewContext"/> types.
    ///</summary>
    public static class ViewExtensions {
        /// <summary>
        /// Gets the current <see cref="ITurbineApplication"/> associated with the MVC application.
        /// </summary>
        /// <param name="viewContext">Current view context.</param>
        /// <returns>Current <see cref="ITurbineApplication"/> or null if not applicable.</returns>
        internal static ITurbineApplication TurbineApplication(this ViewContext viewContext) {
            HttpContextBase httpContext = viewContext.HttpContext;
            if (httpContext == null) return null;

            return httpContext.ApplicationInstance as ITurbineApplication;
        }

        /// <summary>
        /// Gets the current <see cref="IRotorContext"/> associated with the MVC application.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <returns>Current <see cref="IRotorContext"/> or null if not applicable.</returns>
        internal static IRotorContext RotorContext(this ViewContext viewContext) {
            ITurbineApplication turbineApplication = TurbineApplication(viewContext);
            return turbineApplication == null ? null : turbineApplication.CurrentContext;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceLocator"/> associated with the MVC application.
        /// </summary>
        /// <param name="viewContext">Current ViewContext.</param>
        /// <returns>Current <see cref="IServiceLocator"/> or null if not applicable</returns>
        public static IServiceLocator ServiceLocator(this ViewContext viewContext) {
            ITurbineApplication turbineApplication = TurbineApplication(viewContext);
            return turbineApplication == null ? null : turbineApplication.ServiceLocator;
        }
    }
}
