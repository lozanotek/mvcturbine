namespace MvcTurbine.Web.Modules {
    using System;

    [Serializable]
    public class HttpModule {
        public Type Type { get; set; }
        public bool IsRemoved {get; set;}

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

            return ReferenceEquals(this, other) || Equals(other.Type, Type);
        }

        public override int GetHashCode() {
            unchecked {
                return ((Type != null ? Type.GetHashCode() : 0)*397);
            }
        }
    }
}