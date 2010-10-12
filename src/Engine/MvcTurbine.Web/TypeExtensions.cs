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

namespace MvcTurbine.Web {
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Extension method classes
    /// </summary>
    public static class TypeExtensions {

        /// <summary>
        /// Checks to see if the specified type is an MVC filter.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsMvcFilter(this Type type) {
            return typeof(IAuthorizationFilter).IsAssignableFrom(type)
                   || typeof(IActionFilter).IsAssignableFrom(type)
                   || typeof(IResultFilter).IsAssignableFrom(type)
                   || typeof(IExceptionFilter).IsAssignableFrom(type);
        }

        /// <summary>
        /// Checks to see if the specified type is a <see cref="IController"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsController(this Type type) {
            return typeof(IController).IsAssignableFrom(type);
        }
    }
}
