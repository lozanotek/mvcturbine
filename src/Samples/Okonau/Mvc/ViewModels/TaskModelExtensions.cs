namespace Okonau.Mvc.ViewModels {
    using System.Collections.Generic;
    using AutoMapper;
    using Domain;

    /// <summary>
    /// Extensions for <see cref="Task"/> and related view models.
    /// </summary>
    public static class TaskModelExtensions {
        /// <summary>
        /// Static constructor
        /// </summary>
        static TaskModelExtensions() {
            Mapper.CreateMap<Task, TaskViewModel>();
        }

        /// <summary>
        /// Converts the list of <see cref="Task"/> into list of <see cref="TaskViewModel"/>.
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static IList<TaskViewModel> ToViewModel(this IEnumerable<Task> tasks) {
            var list = new List<TaskViewModel>();
            foreach (Task task in tasks) {
                list.Add(task.ToViewModel());
            }

            return list;
        }

        /// <summary>
        /// Converst a <see cref="Task"/> into a <see cref="TaskViewModel"/>.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static TaskViewModel ToViewModel(this Task task) {
            return Mapper.Map<Task, TaskViewModel>(task);
        }
    }
}
