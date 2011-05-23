namespace Okonau.Mvc.Services {
    using System.Collections.Generic;
    using Domain;
    using ViewModels;

    /// <summary>
    /// Defines the domain operations to perform within the system.
    /// </summary>
    public interface ITaskService {
        /// <summary>
        /// Gets all tasks in the system.
        /// </summary>
        /// <returns></returns>
        IList<TaskViewModel> GetTasks();

        /// <summary>
        /// Deletes the specified task from the system.
        /// </summary>
        /// <param name="taskId"></param>
        void DeleteTask(int taskId);

        /// <summary>
        /// Creates a new <see cref="Task"/> with the given information.
        /// </summary>
        /// <param name="model"></param>
        void CreateNewTask(TaskInputModel model);
    }
}
