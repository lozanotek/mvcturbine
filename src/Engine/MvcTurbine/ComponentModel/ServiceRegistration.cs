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

    ///<summary>
    /// Defines a registration for a service within application.
    ///</summary>
    [Serializable]
    public class ServiceRegistration {
        /// <summary>
        /// Gets or sets the type of the service to use.
        /// </summary>
        public Type ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the actual registration handler for the service
        /// </summary>
        public Action<IServiceLocator, Type> RegistrationHandler { get; set; }

        /// <summary>
        /// Gets or sets the filter, if any, to use for the types.
        /// </summary>
        public Func<Type, Type, bool> TypeFilter { get; set; }

        /// <summary>
        /// Checks wether the instance is valid for processing
        /// </summary>
        /// <returns></returns>
        public bool IsValid() {
            return (ServiceType != null && RegistrationHandler != null) && TypeFilter != null;
        }
    }
}