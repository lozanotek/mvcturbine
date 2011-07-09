namespace MvcTurbine.Web.Filters {
    using System;
    using System.Web.Mvc;

    ///<summary>
    /// Simple class for registering an action based filters.
    ///</summary>
    [Serializable]
    public class ActionFilter : Filter {
        /// <summary>
        /// Gets or sets the type of the controller it's associated with.
        /// </summary>
        public Type ControllerType { get; set; }

        /// <summary>
        /// Gets or sets the name of the action it's associated with.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Gets the associated <see cref="FilterScope"/>.
        /// </summary>
        public override FilterScope  Scope {
            get {
                return string.IsNullOrEmpty(Action) ? 
                    FilterScope.Controller : FilterScope.Action;
            }
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof (ActionFilter) && Equals((ActionFilter) obj);
        }

        public bool Equals(ActionFilter other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.ControllerType, ControllerType) && Equals(other.FilterType, FilterType);
        }

        public override int GetHashCode() {
            unchecked {
                return ((ControllerType != null ? ControllerType.GetHashCode() : 0)*397) ^ (FilterType != null ? FilterType.GetHashCode() : 0);
            }
        }
    }
}
