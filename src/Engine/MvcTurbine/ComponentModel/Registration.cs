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
    /// Helper methods for registration of services with the runtime.
    /// </summary>
    public static class Registration {
        /// <summary>
        /// Creates a registration for <see cref="TService"/> with <see cref="RegistrationFilters.DefaultFilter"/>.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static ServiceRegistration Simple<TService>()
            where TService : class {
            return Simple<TService>(RegistrationFilters.DefaultFilter);
        }

        /// <summary>
        /// Creates a registration for <see cref="TService"/> with the specified filter. 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static ServiceRegistration Simple<TService>(Func<Type, Type, bool> filter)
            where TService : class {
            return Custom<TService>(filter,
                                    (locator, type) => locator.Register<TService>(type));
        }

        /// <summary>
        /// Creates a keyed registration for <see cref="TService"/> with <see cref="RegistrationFilters.DefaultFilter"/>.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static ServiceRegistration Keyed<TService>()
            where TService : class {
            return Keyed<TService>(RegistrationFilters.DefaultFilter);
        }

        /// <summary>
        /// Creates a keyed registration for <see cref="TService"/> with the specified filter.        
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static ServiceRegistration Keyed<TService>(Func<Type, Type, bool> filter)
            where TService : class {
            return Custom<TService>(RegistrationFilters.DefaultFilter,
                                    (locator, type) => locator.Register(type.FullName.ToLower(), type));
        }

        /// <summary>
        /// Creates a custom registration for <see cref="TService"/> with the specified filter. 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="filter"></param>
        /// <param name="regAction"></param>
        /// <returns></returns>
        public static ServiceRegistration Custom<TService>(Func<Type, Type, bool> filter, Action<IServiceLocator, Type> regAction)
            where TService : class {

            return new ServiceRegistration {
                TypeFilter = filter,
                RegistrationHandler = regAction,
                ServiceType = typeof(TService)
            };
        }
    }
}