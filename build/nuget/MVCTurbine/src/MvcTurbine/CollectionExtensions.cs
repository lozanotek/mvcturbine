namespace MvcTurbine {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods for collections.
    /// </summary>
    public static class CollectionExtensions {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action) {
            if (action == null) {
                throw new ArgumentNullException("action");
            }

            foreach (T item in collection) {
                action(item);
            }
        }
    }
}