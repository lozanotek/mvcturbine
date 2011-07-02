namespace MvcTurbine.Web.Modules {
    using System;

    /// <summary>
    /// Defines the structure for Http module registration.
    /// </summary>
    [Serializable]
    public class HttpModule {
        /// <summary>
        /// Gets or sets the type for the module.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets whether the module should be removed.
        /// </summary>
        public bool IsRemoved {get; set;}

        /// <summary>
        /// Get or sets the name for the module.
        /// </summary>
        public string Name { get; set; }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj.GetType() == typeof (HttpModule) && Equals((HttpModule) obj);
        }

        public bool Equals(HttpModule other) {
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
