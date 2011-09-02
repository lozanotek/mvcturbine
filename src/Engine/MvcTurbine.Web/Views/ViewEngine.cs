namespace MvcTurbine.Web.Views {
    using System;

    /// <summary>
    /// Simple component that defines the registration of a view engine.
    /// </summary>
    [Serializable]
    public class ViewEngine {
        /// <summary>
        /// Gets or sets the type of the view engine.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the view engine.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the bit flag for a removed engine.
        /// </summary>
        public bool IsRemoved { get; set; }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(ViewEngine) && Equals((ViewEngine)obj);
        }

        public bool Equals(ViewEngine other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }

            return ReferenceEquals(this, other) || Equals(other.Name, Name);
        }

        public override int GetHashCode() {
            unchecked {
                return (Name != null ? Name.GetHashCode() : 0);
            }
        }
    }
}
