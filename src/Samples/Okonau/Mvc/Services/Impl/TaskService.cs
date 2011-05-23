namespace Okonau.Mvc.Services {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Persistence;
    using ViewModels;

    /// <summary>
    /// Defines the domain operations for a task.
    /// </summary>
    public class TaskService : ITaskService {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="repository"></param>
        public TaskService(IRepository<Task> repository) {
            TaskRepository = repository;
        }

        /// <summary>
        /// Gets the associated <see cref="IRepository{T}"/> with the service.
        /// </summary>
        public IRepository<Task> TaskRepository { get; private set; }

        /// <summary>
        /// Gets all the tasks in the system.
        /// </summary>
        /// <returns></returns>
        public IList<TaskViewModel> GetTasks() {
            var tasks = from task in TaskRepository
                        orderby task.Created descending
                        select task;

            return tasks.ToViewModel();
        }

        /// <summary>
        /// Deletes the specified task from the system.
        /// </summary>
        /// <param name="taskId"></param>
        public void DeleteTask(int taskId) {
            Task found = TaskRepository
                .Where(task => task.Id == taskId)
                .SingleOrDefault();

            if (found == null) return;
            TaskRepository.Remove(found);
        }

        /// <summary>
        /// Creates a new <see cref="Task"/> with the given information.
        /// </summary>
        /// <param name="model"></param>
        public void CreateNewTask(TaskInputModel model) {
            if (model == null) return;

            var task = new Task
            {
                Description = model.Description,
                Name = model.Name,
                Created = DateTime.Now
            };

            TaskRepository.Add(task);
        }
    }
}
