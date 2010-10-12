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

    /// <summary>
    /// Defines the missing resolution of services within the <see cref="IServiceLocator"/>.
    /// </summary>
    [Serializable]
    public class ServiceResolutionException : Exception {
        /// <summary>
        ///  Creates an exception with the specified type.
        /// </summary>
        /// <param name="service"></param>
        public ServiceResolutionException(Type service) :
            base(string.Format("Could not resolve serviceType '{0}'", service)) {
            ServiceType = service;
        }

        /// <summary>
        /// Creates an exception with the specified type and inner exception. 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="innerException"></param>
        public ServiceResolutionException(Type service, Exception innerException)
            : base(string.Format("Could not resolve serviceType '{0}'", service), innerException) {
            ServiceType = service;
        }

        /// <summary>
        /// Gets or sets the type of the service to use.
        /// </summary>
        public Type ServiceType { get; set; }
    }
}
