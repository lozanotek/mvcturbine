namespace Okonau.Mvc.ViewModels {
    using System;

    /// <summary>
    /// Displays the information for a task.
    /// </summary>
    [Serializable]
    public class TaskViewModel {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        public DateTime Created { get; set; }
    }
}
