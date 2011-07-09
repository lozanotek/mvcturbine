namespace MvcTurbine {
    using System;
    
    /// <summary>
    /// Extension method classes
    /// </summary>
    public static class TypeExtensions {

        /// <summary>
        /// Checks to see if the specified type is assignable.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsType<TType>(this Type type) {
            return typeof (TType).IsAssignableFrom(type);
        }

        /// <summary>
        /// Checks to see if the specified type is assignable.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsType<TType>(this object obj) {
            return IsType<TType>(obj.GetType());
        }

        /// <summary>
        /// Checks to see if the specified type is assignable.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsType(this object obj, Type type) {
            return type != null && type.IsAssignableFrom(obj.GetType());
        }
    }
}