namespace MvcTurbine.ComponentModel {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Simple type cache for the system to use.
    /// </summary>
    [Serializable]
    public class TypeCache : Dictionary<Type, Type> {
        /// <summary>
        /// Merges the specified <see cref="TypeCache"/> with the current instance.
        /// </summary>
        /// <param name="cache"></param>
        public void Merge(TypeCache cache) {
            if (cache == null) return;

            foreach (var pair in cache) {
                Add(pair);
            }
        }

        /// <summary>
        /// Adds the key value pair with the current instance.
        /// </summary>
        /// <param name="pair"></param>
        public void Add(KeyValuePair<Type, Type> pair) {
            Add(pair.Key, pair.Value);
        }
    }
}
