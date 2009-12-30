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
