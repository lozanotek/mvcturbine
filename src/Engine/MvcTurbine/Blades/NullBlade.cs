namespace MvcTurbine.Blades {
    using System;

    /// <summary>
    /// Represents a Null instance of a Blade (Null-Pattern)
    /// </summary>
    [Serializable]
    public sealed class NullBlade : Blade {
        private static readonly NullBlade instance = new NullBlade();

        /// <summary>
        /// Private constructor to prevent object creation
        /// </summary>
        private NullBlade() {
        }

        public override void Spin(IRotorContext context) {
            //Do no work!
        }

        /// <summary>
        /// Gets an instance of the <seealso cref="NullBlade"/>.
        /// </summary>
        public static NullBlade Instance {
            get { return instance; }
        }
    }
}