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

    /// <summary>
    /// Default <see cref="IModelBinder"/> to use within an application.
    /// </summary>
    public class TurbineModelBinder : DefaultModelBinder {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="locator"></param>
        public TurbineModelBinder(IServiceLocator locator, IBinderRegistrationManager binderManager) {
            if (locator == null) {
                throw new ArgumentNullException("locator", "The specified IServiceLocator cannot be null.");
            }

            if (binderManager == null) {
                throw new ArgumentNullException("binderManager", "The specified IBinderRegistrationManager cannot be null.");
            }

            ServiceLocator = locator;
            BinderManager = binderManager;
        }

        /// <summary>
        /// Gets the <see cref="IServiceLocator"/> to use.
        /// </summary>
        public IServiceLocator ServiceLocator { get; private set; }

        /// <summary>
        /// Gets the <see cref="IBinderRegistrationManager"/> to use.
        /// </summary>
        public IBinderRegistrationManager BinderManager { get; private set; }

        /// <summary>
        /// Processes the registered <see cref="IModelBinder"/> within the <see cref="ServiceLocator"/>.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            var registeredModelBinders = GetRegisteredModelBinders();

            if (registeredModelBinders == null || registeredModelBinders.Count == 0) {
                return base.BindModel(controllerContext, bindingContext);
            }

            var modelType = bindingContext.ModelMetadata.ModelType;
            var binderType = BinderManager.GetModelBinderForType(modelType);

            if (binderType != null) {
                var foundBinder = registeredModelBinders
                    .Where(binder => binder.GetType().IsAssignableFrom(binderType))
                    .FirstOrDefault();

                if (foundBinder != null) {
                    //TODO: Can be better addressed with nested container
                    var result = foundBinder.BindModel(controllerContext, bindingContext);
                    ServiceLocator.Release(foundBinder);
                    return result;
                }

            }

            foreach (var binder in registeredModelBinders) {
                var filterableBinder = binder as IFilterableModelBinder;
                if (filterableBinder == null) continue;

                if (!filterableBinder.SupportsModelType(modelType)) continue;

                //TODO: Can be better addressed with nested container
                var result = binder.BindModel(controllerContext, bindingContext);
                ServiceLocator.Release(binder);
                return result;
            }

            return base.BindModel(controllerContext, bindingContext);
        }

        /// <summary>
        /// Gets the current registered <see cref="IModelBinder"/> instances from the container
        /// and caches them.
        /// </summary>
        /// <returns>Cached list of <see cref="IModelBinder"/>, if cache null, <see cref="IServiceLocator"/> 
        /// is queried and results are cached.</returns>
        protected virtual IList<IModelBinder> GetRegisteredModelBinders() {
            try {
                return ServiceLocator.ResolveServices<IModelBinder>();
            } catch {
                return null;
            }
        }
    }
}
