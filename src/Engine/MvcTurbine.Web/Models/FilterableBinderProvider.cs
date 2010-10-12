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

namespace MvcTurbine.Web.Models {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ComponentModel;

    public class FilterableBinderProvider : IModelBinderProvider {
        public IServiceLocator ServiceLocator { get; private set; }
        private static IList<IFilterableModelBinder> registeredBinders;
        private static object _lock = new object();

        public FilterableBinderProvider(IServiceLocator serviceLocator) {
            ServiceLocator = serviceLocator;
        }

        public IModelBinder GetBinder(Type modelType) {
            var registeredModelBinders = GetRegisteredModelBinders();
            return registeredModelBinders == null ? null :
                registeredModelBinders.FirstOrDefault(filterableBinder => filterableBinder.SupportsModelType(modelType));
        }

        /// <summary>
        /// Gets the current registered <see cref="IModelBinder"/> instances from the container
        /// and caches them.
        /// </summary>
        /// <returns>Cached list of <see cref="IModelBinder"/>, if cache null, <see cref="IServiceLocator"/> 
        /// is queried and results are cached.</returns>
        protected virtual IList<IFilterableModelBinder> GetRegisteredModelBinders() {
            if (registeredBinders == null) {
                lock (_lock) {
                    if (registeredBinders == null) {
                        try {
                            registeredBinders = ServiceLocator.ResolveServices<IFilterableModelBinder>();
                        } catch {
                            registeredBinders = new List<IFilterableModelBinder>();
                        }
                    }
                }
            }

            return registeredBinders;
        }
    }
}
