namespace Okonau.Mvc.ViewModels {
    using System;

    /// <summary>
    /// Defines input information for the new task.
    /// </summary>
    [Serializable]
    public class TaskInputModel {

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
    }
}
