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

namespace MvcTurbine.ComponentModel {

    ///<summary>
    /// Defines the process of doing auto registration of a specified service type.
    ///</summary>
    public interface IAutoRegistrator {
        /// <summary>
        /// Gets or sets the <seealso cref="AssemblyFilter"/> to use.
        /// </summary>
        AssemblyFilter Filter { get; set; }

        /// <summary>
        /// Process the specified <seealso cref="ServiceRegistration"/> for the types in all assemblies.
        /// </summary>
        /// <param name="serviceRegistration">Instance of <see cref="ServiceRegistration"/> to use.</param>
        void AutoRegister(ServiceRegistration serviceRegistration);
    }
}