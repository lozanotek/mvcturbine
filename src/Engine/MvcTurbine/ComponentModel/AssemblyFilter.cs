using System.Text.RegularExpressions;

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
                if (Regex.IsMatch(assemblyName, filter)) {
                    return true;
                }
            }

            return false;
        }
    }
}