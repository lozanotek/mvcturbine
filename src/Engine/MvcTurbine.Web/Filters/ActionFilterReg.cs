namespace MvcTurbine.Web.Filters {
    using System;
    using System.Web.Mvc;

    ///<summary>
    /// Simple class for registering an action based filters.
    ///</summary>
    [Serializable]
    public class ActionFilterReg : FilterReg {
        /// <summary>
        /// Gets or sets the type of the controller it's associated with.
        /// </summary>
        public Type Controller { get; set; }

        /// <summary>
        /// Gets or sets the name of the action it's associated with.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Gets the associated <see cref="FilterScope"/>.
        /// </summary>
        public override FilterScope  Scope {
            get { return FilterScope.Action; }
        }
    }
}