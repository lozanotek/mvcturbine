namespace MvcTurbine.Blades {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a list of <see cref="IBlade"/> types to use or process.
    /// </summary>
    [Serializable]
    public class BladeList : List<IBlade> {
        /// <summary>
        /// Default constructor
        /// </summary>
        public BladeList() {
        }

        /// <summary>
        /// Creates a new lis with the specified <see cref="IEnumerable{T}"/> collection.
        /// </summary>
        /// <param name="blades">Enumerable type that contains <see cref="IBlade"/> instances.</param>
        public BladeList(IEnumerable<IBlade> blades)
            : base(blades) {
        }
    }
}