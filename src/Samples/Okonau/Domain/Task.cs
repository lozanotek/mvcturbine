namespace Okonau.Domain {
    using System;

    /// <summary>
    /// Defines a Task for the system to use.
    /// </summary>
    [Serializable]
    public class Task {

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public virtual string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public virtual string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the Created date time.
        /// </summary>
        public virtual DateTime Created { get; set; }
    }
}
