using System;
using System.Linq;
using System.Text;

namespace MvcTurbine.Web.Views
{
    [Serializable]
    public class ViewEngine {
        public Type Type { get; set; }
        public string Name { get; set; }
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
