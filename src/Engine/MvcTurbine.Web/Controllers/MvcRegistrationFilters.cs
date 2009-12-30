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

namespace MvcTurbine.Web.Controllers {
    using System;
    using System.Web.Mvc;
    using ComponentModel;
    using Models;

    /// <summary>
    /// Helper class for registration of ASP.NET MVC pieces.
    /// </summary>
    public static class MvcRegistrationFilters {

        /// <summary>
        /// Gets the registration for a <see cref="IModelBinder"/>.
        /// </summary>
        public static Func<Type, Type, bool> ModelBinderFilter {
            get {
                Func<Type, Type, bool> filter = RegistrationFilters.DefaultFilter;

                return (serviceType, registrationType) =>
                    !serviceType.IsType<TurbineModelBinder>() &&
                       filter(serviceType, registrationType);
            }
        }
    }
}