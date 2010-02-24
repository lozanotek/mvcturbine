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
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Default implementation of <seealso cref="IAutoRegistrator"/>.
    /// </summary>
    public class DefaultAutoRegistrator : IAutoRegistrator {
        private static readonly object _lock = new object();

        /// <summary>
        /// Creates an instance with the specified <seealso cref="IServiceLocator"/> implementation.
        /// </summary>
        /// <param name="locator"></param>
        public DefaultAutoRegistrator(IServiceLocator locator) {
            ServiceLocator = locator;
            Filter = new CommonAssemblyFilter();
        }

        /// <summary>
        /// Gets the <seealso cref="IServiceLocator"/> associated with this instance.
        /// </summary>
        public IServiceLocator ServiceLocator { get; private set; }

        /// <summary>
        /// Gets or sets the <seealso cref="AssemblyFilter"/> to use.
        /// </summary>
        public AssemblyFilter Filter { get; set; }

        /// <summary>
        /// Process the specified <seealso cref="ServiceRegistration"/> for the types in all assemblies.
        /// </summary>
        /// <param name="serviceRegistration">Instance of <see cref="ServiceRegistration"/> to use.</param>
        public void AutoRegister(ServiceRegistration serviceRegistration) {
            if (serviceRegistration == null ||
                !serviceRegistration.IsValid()) return;

            lock (_lock) {
                var assemblies = GetAssemblies();
                var registration = serviceRegistration.RegistrationHandler;
                var serviceType = serviceRegistration.ServiceType;
                var typeFilter = serviceRegistration.TypeFilter;

                foreach (Assembly assembly in assemblies) {
                    try {
                        var registrationTypes = assembly.GetTypes()
                            .Where(matchedType => typeFilter(matchedType, serviceType));

                        foreach (Type type in registrationTypes) {
                            registration(ServiceLocator, type);
                        }
                    } catch (ReflectionTypeLoadException loadException) {
                        string assemblyName = assembly.FullName;
                        string detailedMessage = loadException.GetDetailedMessage(assemblyName);

                        throw new DependencyResolutionException(assemblyName, detailedMessage);
                    }
                }
            }
        }

        /// <summary>
        /// Gets all the assemblies after the <see cref="Filter"/> property is applied.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Assembly> GetAssemblies() {
            if (Filter != null) {
                return AppDomain.CurrentDomain.GetAssemblies()
                    .Where(assembly => !Filter.Match(assembly.FullName));
            }

            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}