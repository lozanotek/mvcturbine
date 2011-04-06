namespace MvcTurbine.Web.Views {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the a tabular structure of embedded views keyed off by their name.
    /// </summary>
    [Serializable]
    public class EmbeddedViewTable {
        private static readonly object _lock = new object();
        private readonly Dictionary<string, EmbeddedView> viewCache;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EmbeddedViewTable() {
            viewCache = new Dictionary<string, EmbeddedView>(StringComparer.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Adds the view by name and Assembly name.
        /// </summary>
        /// <param name="viewName">Name of the view to add.</param>
        /// <param name="assemblyName">Name of the assembly to add.</param>
        public void AddView(string viewName, string assemblyName) {
            lock (_lock) {
                viewCache[viewName] = new EmbeddedView { Name = viewName, AssemblyFullName = assemblyName };
            }
        }

        /// <summary>
        /// Gets the list of Views in the table.
        /// </summary>
        public IList<EmbeddedView> Views {
            get {
                return viewCache.Values.ToList();
            }
        }

        /// <summary>
        /// Checks whether the specified view exists within the viewPath.
        /// </summary>
        /// <param name="viewPath"></param>
        /// <returns></returns>
        public bool ContainsEmbeddedView(string viewPath) {
            var foundView = FindEmbeddedView(viewPath);
            return (foundView != null);
        }

        /// <summary>
        /// Searches the registered views for the specified one.
        /// </summary>
        /// <param name="viewPath">Path of the view.</param>
        /// <returns></returns>
        public EmbeddedView FindEmbeddedView(string viewPath) {
            var name = GetNameFromPath(viewPath);
            if (string.IsNullOrEmpty(name)) return null;

            return Views.Where(view => view.Name.Contains(name)).SingleOrDefault();
        }

        /// <summary>
        /// Cleans up the name of the view from the specified path.
        /// </summary>
        /// <param name="viewPath"></param>
        /// <returns></returns>
        protected string GetNameFromPath(string viewPath) {
            if (string.IsNullOrEmpty(viewPath)) return null;
            var name = viewPath.Replace("/", ".");
            return name.Replace("~", "");
        }
    }
}
