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
    using System;
    using System.Collections;
    using System.Collections.Generic;

    ///<summary>
    /// Defines a list of auto-registrations for the system process.
    ///</summary>
    [Serializable]
    public class AutoRegistrationList : IEnumerable<ServiceRegistration> {

        private readonly List<ServiceRegistration> registrationList;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AutoRegistrationList() {
            registrationList = new List<ServiceRegistration>();
        }

        /// <summary>
        /// Adds the specified <see cref="ServiceRegistration"/>
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public AutoRegistrationList Add(ServiceRegistration registration) {
            registrationList.Add(registration);
            return this;
        }

        /// <summary>
        /// Clears the current list.
        /// </summary>
        public void Clear() {
            registrationList.Clear();
        }

        /// <summary>
        /// Gets the enumerator of <seealso cref="ServiceRegistration"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ServiceRegistration> GetEnumerator() {
            return registrationList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}