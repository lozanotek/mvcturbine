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

namespace MvcTurbine.ComponentModel {
    /// <summary>
    /// Extension methods for <see cref="IServiceLocator"/>.
    /// </summary>
    public static class ServiceLocatorExtensions {
        /// <summary>
        /// Gets the associated container with the current <see cref="IServiceLocator"/> instance.
        /// </summary>
        /// <typeparam name="TContainer"></typeparam>
        /// <param name="locator"></param>
        /// <returns></returns>
        public static TContainer GetUnderlyingContainer<TContainer>(this IServiceLocator locator)
            where TContainer : class {
            if (locator == null) return null;

            var property = locator.GetType().GetProperty("Container");
            if (property == null) return null;

            return property.GetValue(locator, null) as TContainer;
        }
    }
}
