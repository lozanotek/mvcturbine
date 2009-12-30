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

namespace Okonau.Persistence.Mappings {
    using Domain;
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Defines the mapping between <see cref="Task"/> and the persistence table.
    /// </summary>
    public class TaskMapping : ClassMap<Task> {
        public TaskMapping() {
            // Specify the table to use
            Table("Tasks");

            // Sets the key for the table to use
            Id(x => x.Id)
                .GeneratedBy
                .Increment();
            
            // Maps all the columns
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Created, "CreatedDate");
        }
    }
}
