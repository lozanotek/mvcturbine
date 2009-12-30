#region License

//
// Author: Javier Lozano <javier@lozanotek.com>
// Copyright (c) 2009-2010, lozanotek, inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

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

        #region ITaskService Members

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

        #endregion
    }
}
