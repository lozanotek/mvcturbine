namespace MvcTurbine.Web.Views {
    using System;

    /// <summary>
    /// Defines an embedded view.
    /// </summary>
    [Serializable]
    public class EmbeddedView {
        /// <summary>
        /// Gets or sets the name of the view.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the full name of the Assembly that houses the view.
        /// </summary>
        public string AssemblyFullName { get; set; }
    }
}