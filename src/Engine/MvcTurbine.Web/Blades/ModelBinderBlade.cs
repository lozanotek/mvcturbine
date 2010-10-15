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

namespace MvcTurbine.Web.Blades {
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ComponentModel;
    using Controllers;
    using Models;
    using MvcTurbine.Blades;

    /// <summary>
    /// Blade for all the <see cref="IModelBinder"/> components.
    /// </summary>
    public class ModelBinderBlade : Blade, ISupportAutoRegistration {
        public override void Spin(IRotorContext context) {
            // Get the current IServiceLocator
            var serviceLocator = GetServiceLocatorFromContext(context);

            SetupBinderProviders(serviceLocator);
            SetupBinderRegistries(serviceLocator);
        }

        public virtual void SetupBinderRegistries(IServiceLocator serviceLocator) {
            var binderRegistries = GetBinderRegistries(serviceLocator);
            if (binderRegistries == null) return;

            var aggregateCache = new TypeCache();

            foreach (var modelBinderRegistry in binderRegistries) {
                var binderCache = modelBinderRegistry.GetBinderRegistrations();
                if (binderCache == null) continue;

                // We will register auto-register the binders for you
                using (serviceLocator.Batch()) {
                    foreach (var binderType in binderCache.Values) {
                        serviceLocator.Register(binderType, binderType);
                    }
                }

                aggregateCache.Merge(binderCache);
            }

            ModelBinderProviders.BinderProviders.Add(new ModelBinderRegistryProvider(serviceLocator, aggregateCache));
        }

        public virtual void SetupBinderProviders(IServiceLocator serviceLocator) {
            var binderProviders = GetModelBinderProviders(serviceLocator);

            if (binderProviders != null && binderProviders.Count != 0) {
                foreach (var binderProvider in binderProviders) {
                    ModelBinderProviders.BinderProviders.Add(binderProvider);
                }
            }

            ModelBinderProviders.BinderProviders.Add(new FilterableBinderProvider(serviceLocator));
        }

        public void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList
                .Add(MvcRegistration.RegisterBinder())
                .Add(Registration.Simple<IModelBinderProvider>())
                .Add(Registration.Simple<ModelBinderRegistry>());
        }

        protected virtual IList<IModelBinderProvider> GetModelBinderProviders(IServiceLocator locator) {
            try {
                return locator.ResolveServices<IModelBinderProvider>();
            } catch {
                return null;
            }
        }

        protected virtual IList<ModelBinderRegistry> GetBinderRegistries(IServiceLocator locator) {
            try {
                return locator.ResolveServices<ModelBinderRegistry>();
            } catch {
                return null;
            }
        }
    }
}