namespace MvcTurbine.Web.Views {
    using System;
    using System.Collections;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Hosting;

    /// <summary>
    /// VirtualPathProvider for embedded views.
    /// </summary>
    public class EmbeddedViewVirtualPathProvider : VirtualPathProvider {
        private readonly EmbeddedViewTable embeddedViews;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="table">Table of views to use for resolution.</param>
        public EmbeddedViewVirtualPathProvider(EmbeddedViewTable table) {
            if (table == null) {
                throw new ArgumentNullException("table", "EmbeddedViewTable cannot be null.");
            }

            embeddedViews = table;
        }

        /// <summary>
        /// Checks whether the requested virtual path is an embedded view.
        /// </summary>
        /// <param name="virtualPath">Virtual path to check.</param>
        /// <returns></returns>
        protected virtual bool IsEmbeddedView(string virtualPath) {
            string checkPath = VirtualPathUtility.ToAppRelative(virtualPath);

            return checkPath.StartsWith("~/Views/", StringComparison.InvariantCultureIgnoreCase) 
                   && embeddedViews.ContainsEmbeddedView(checkPath);
        }

        /// <summary>
        /// Checks whether the embedded view "exists" within an Assembly.
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public override bool FileExists(string virtualPath) {
            return (IsEmbeddedView(virtualPath) ||
                    base.FileExists(virtualPath));
        }

        /// <summary>
        /// Gets the <see cref="VirtualFile"/> that's associated with the path.
        /// </summary>
        /// <param name="virtualPath">VirtualPath to check</param>
        /// <returns></returns>
        public override VirtualFile GetFile(string virtualPath) {
            if (IsEmbeddedView(virtualPath)) {
                EmbeddedView embeddedView = embeddedViews.FindEmbeddedView(virtualPath);
                return new AssemblyResourceFile(embeddedView, virtualPath);
            }

            return base.GetFile(virtualPath);
        }

        /// <summary>
        /// Gets the cache dependency for a virtual path.
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <param name="virtualPathDependencies"></param>
        /// <param name="utcStart"></param>
        /// <returns>If the view is embedded, always return null. Otherwise return what the base specifies.</returns>
        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart) {
            return IsEmbeddedView(virtualPath) ? null : 
                base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }
    }
}
