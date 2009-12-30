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
    using System.Web.Mvc;
    using ComponentModel;

    /// <summary>
    /// Default <see cref="IModelBinder"/> to use within an application.
    /// </summary>
    public class TurbineModelBinder : DefaultModelBinder {

        private static readonly object _lock = new object();

        /// <summary>
        /// Cache list for the model binders to use
        /// </summary>
        private IList<IFilterableModelBinder> modelBinders;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="locator"></param>
        public TurbineModelBinder(IServiceLocator locator) {
            if (locator == null) {
                throw new ArgumentNullException("locator", "The specified IServiceLocator cannot be null.");
            }

            ServiceLocator = locator;
        }

        /// <summary>
        /// Gets the current ServiceLocator associated with this binder
        /// </summary>
        public IServiceLocator ServiceLocator { get; private set; }

        /// <summary>
        /// Processes the registered <see cref="IModelBinder"/> within the <see cref="ServiceLocator"/>.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            var binders = GetRegisteredModelBinders();

            if (binders == null || binders.Count == 0) {
                return base.BindModel(controllerContext, bindingContext);
            }

            foreach (var binder in binders) {
                if (!binder.SupportsModelType(bindingContext.ModelType)) continue;

                return binder.BindModel(controllerContext, bindingContext);
            }

            return base.BindModel(controllerContext, bindingContext);
        }

        /// <summary>
        /// Gets the current registered <see cref="IFilterableModelBinder"/> instances from the container
        /// and caches them.
        /// </summary>
        /// <returns>Cached list of <see cref="IFilterableModelBinder"/>, if cache null, <see cref="IServiceLocator"/> 
        /// is queried and results are cached.</returns>
        //THOUGHT: Might need to remve this piece when we have dynamic addition of the model binders.       
        protected virtual IList<IFilterableModelBinder> GetRegisteredModelBinders() {
            if (modelBinders == null) {
                lock (_lock) {
                    if (modelBinders == null) {
                        modelBinders = ServiceLocator.ResolveServices<IFilterableModelBinder>();
                    }
                }
            }

            return modelBinders;
        }
    }
}