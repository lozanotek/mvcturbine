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
    using System.Linq.Expressions;

    /// <summary>
    /// Helper methods for filtering types within the framework.
    /// </summary>
    public static class RegistrationFilters {
        private static readonly Func<Type, Type, bool> defaultFilter = BuildDefaultFilter();

        /// <summary>
        /// Gets the default filter for the system to use.
        /// </summary>
        public static Func<Type, Type, bool> DefaultFilter {
            get { return defaultFilter; }
        }

        private static Func<Type, Type, bool> BuildDefaultFilter() {
            //HACK: some types in MVC (filters, etc.) are defined as System.Attribute, so we need to ommit those.
            Type attrType = typeof (Attribute);

            return (serviceType, registrationType) =>
                   !attrType.IsAssignableFrom(serviceType) &&
                   registrationType.IsAssignableFrom(serviceType) &&
                   serviceType != registrationType &&
                   !serviceType.IsAbstract &&
                   !serviceType.IsGenericTypeDefinition &&
                   !serviceType.ContainsGenericParameters;
        }
    }
}
