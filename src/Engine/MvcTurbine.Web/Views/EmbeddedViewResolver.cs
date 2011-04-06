namespace MvcTurbine.Web.Views
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Gets the embedded views within any loaded assembly.
    /// </summary>
    public class EmbeddedViewResolver : IEmbeddedViewResolver
    {
        private static string[] EmbeddedNamespaces = new[] { ".views." };

        /// <summary>
        /// Creates a list of embedded views from currently assemblies in the AppDomain.
        /// </summary>
        /// <returns></returns>
        public virtual EmbeddedViewTable GetEmbeddedViews()
        {
            Assembly[] assemblies = GetAssemblies();
            if (assemblies == null || assemblies.Length == 0) return null;

            var table = new EmbeddedViewTable();

            foreach (var assembly in assemblies)
            {
                var names = GetNamesOfAssemblyResources(assembly);
                if (names == null || names.Length == 0) continue;

                foreach (var name in names)
                {
                    var key = name.ToLowerInvariant();
                    if (!EmbeddedNamespaces.Any(key.Contains)) continue;

                    table.AddView(name, assembly.FullName);
                }
            }

            return table;
        }

        /// <summary>
        /// Gets the current loaded assemblies in to AppDomain.
        /// </summary>
        /// <returns></returns>
        protected virtual Assembly[] GetAssemblies()
        {
            try
            {
                return AppDomain.CurrentDomain.GetAssemblies();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the names for resources within the specified assembly.
        /// </summary>
        /// <param name="assembly">Currently assembly to search.</param>
        /// <returns></returns>
        protected virtual string[] GetNamesOfAssemblyResources(Assembly assembly)
        {
            try
            {
                // GetManifestResourceNames will throw a NotSupportedException when run on a dynamic assembly
                return assembly.GetManifestResourceNames();
            }
            catch
            {
                return new string[] { };
            }
        }
    }
}