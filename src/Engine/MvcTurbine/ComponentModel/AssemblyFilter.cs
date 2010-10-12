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
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a list of filters to apply to an assembly name.
    /// </summary>
    [Serializable]
    public class AssemblyFilter {
        /// <summary>
        /// Public default constructor.
        /// </summary>
        public AssemblyFilter() {
            Filters = new List<string>();
        }

        /// <summary>
        /// Gets or sets the list for the filters.
        /// </summary>
        private List<string> Filters { get; set; }

        /// <summary>
        /// Adds the specified filter to the list if not previously added.
        /// </summary>
        /// <param name="filter">TypeFilter to add into the list.</param>
        public void AddFilter(string filter) {
            if (string.IsNullOrEmpty(filter) ||
                Filters.Contains(filter)) return;

            Filters.Add(filter);
        }

        /// <summary>
        /// Clears the list of registered filters.
        /// </summary>
        public void Clear() {
            Filters.Clear();
        }

        /// <summary>
        /// Checks whether <paramref name="assemblyName"/> matches the filter list.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly to check.</param>
        /// <returns>True if match, false otherise.</returns>
        public bool Match(string assemblyName) {
            if (string.IsNullOrEmpty(assemblyName)) return false;

            foreach (var filter in Filters) {
                if (assemblyName.Contains(filter)) {
                    return true;
                }
            }

            return false;
        }
    }
}