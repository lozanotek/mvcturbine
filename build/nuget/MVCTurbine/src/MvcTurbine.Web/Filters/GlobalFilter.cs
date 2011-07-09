namespace MvcTurbine.Web.Filters {
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Simple registration class for global filters.
    /// </summary>
    [Serializable]
    public class GlobalFilter : Filter {
        /// <summary>
        /// Gets the scope of the filter scope to use.
        /// </summary>
        public override FilterScope Scope {
            get { return FilterScope.Global; }
        }
    }
}