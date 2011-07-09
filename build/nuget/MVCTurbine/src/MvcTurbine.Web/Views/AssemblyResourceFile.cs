namespace MvcTurbine.Web.Views {
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web.Hosting;

    /// <summary>
    /// Defines a resource file that's embedded within an Assembly.
    /// </summary>
    public class AssemblyResourceFile : VirtualFile {
        private readonly EmbeddedView embeddedView;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="view">Associated embedded view.</param>
        /// <param name="virtualPath">Virtual path in question.</param>
        public AssemblyResourceFile(EmbeddedView view, string virtualPath) : base(virtualPath) {
            if(view == null) throw new ArgumentNullException("view", "EmbeddedView cannot be null.");

            embeddedView = view;
        }

        /// <summary>
        /// Gets the stream to the associated resource file.
        /// </summary>
        /// <returns></returns>
        public override Stream Open() {
            Assembly assembly = GetResourceAssembly();
            if (assembly == null) return null;
            
            return assembly.GetManifestResourceStream(embeddedView.Name);
        }

        /// <summary>
        /// Gets the current assembly with the associated resource.
        /// </summary>
        /// <returns></returns>
        protected virtual Assembly GetResourceAssembly() {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            return assemblies
                .Where(assembly => string.Equals(assembly.FullName, embeddedView.AssemblyFullName, StringComparison.InvariantCultureIgnoreCase))
                .SingleOrDefault();
        }
    }
}