namespace MvcTurbine.Web.Filters {
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Base class for registration of filters.
    /// </summary>
    [Serializable]
    public abstract class Filter {
        /// <summary>
        /// Gets or sets the filter type
        /// </summary>
        public Type FilterType { get; set; }

        /// <summary>
        /// Gets or sets the order for the filter.
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// Gets or sets the scope of the filter.
        /// </summary>
        public abstract FilterScope Scope { get; }

        /// <summary>
        /// Gets or sets the initializer for the filter.
        /// </summary>
        public Action<object> ModelInitializer { get; set; }
    }
}
