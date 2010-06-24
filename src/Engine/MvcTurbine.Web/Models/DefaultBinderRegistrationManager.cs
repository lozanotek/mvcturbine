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
    using ComponentModel;

    public class DefaultBinderRegistrationManager : IBinderRegistrationManager {
        private static TypeCache binderCache = new TypeCache();

        public IServiceLocator Locator { get; set; }

        public DefaultBinderRegistrationManager(IServiceLocator locator) {
            Locator = locator;
        }

        public IList<ModelBinderRegistry> BinderRegistries {
            get {
                try {
                    return Locator.ResolveServices<ModelBinderRegistry>();
                } catch {
                    return null;
                }
            }
        }

        public virtual Type GetModelBinderForType(Type modelType) {
            var currentRegistries = BinderRegistries;
            if (currentRegistries == null) return null;

            if (binderCache.Count == 0) {
                foreach (var regTable in currentRegistries.Select(registry => registry.GetBinderRegistrations())) {
                    binderCache.Concat(regTable);
                }
            }

            return !binderCache.ContainsKey(modelType) ? null : binderCache[modelType];
        }
    }
}