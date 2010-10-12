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
    using System.Linq;
    using System.Web.Mvc;
    using ComponentModel;
    using Filters;

    /// <summary>
    /// Defines a custom <see cref="ControllerActionInvoker"/> to use that will 
    /// infer actions that are not defined.
    /// </summary>
    public class TurbineActionInvoker : ControllerActionInvoker {

        private static readonly object _lock = new object();
        
        /// <summary>
        /// Same <see cref="IFilterFinder"/> for all actions
        /// </summary>
        private static IFilterFinder filterFinder;

        ///<summary>
        /// Creates a new instance of the <see cref="ControllerActionInvoker"/> that 
        /// provides action execution for the controller
        ///</summary>
        ///<param name="locator"></param>
        public TurbineActionInvoker(IServiceLocator locator) {
            ServiceLocator = locator;
        }

        /// <summary>
        /// Gets the current instance of <see cref="IServiceLocator"/>.
        /// </summary>
        public IServiceLocator ServiceLocator { get; private set; }

        /// <summary>
        /// Finds the action for the controller, if not it is inferred.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="controllerDescriptor"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        protected override ActionDescriptor FindAction(ControllerContext controllerContext,
            ControllerDescriptor controllerDescriptor, string actionName) {
            
            ActionDescriptor foundAction;

            try {
                // Find the base action
                foundAction = base.FindAction(controllerContext, controllerDescriptor, actionName);
            } catch {
                //HACK: Had to add this piece to support ASP.NET MVC on Mono
                foundAction = null;
            }

            if (foundAction == null) {
                foundAction = new InferredActionDescriptor(actionName, controllerDescriptor);
            }

            return foundAction;
        }

        /// <summary>
        /// Gets the filters for the current <see cref="ActionDescriptor"/>.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor) {
            var defaultFilters = base.GetFilters(controllerContext, actionDescriptor) ?? new FilterInfo();
            InjectDependenciesIntoFilters(defaultFilters);

            var finder = GetFilterFinder();
            var registeredFilters = finder.FindFilters(actionDescriptor);

            return new MergedFilters(defaultFilters, registeredFilters);
        }

        /// <summary>
        /// For each of the filters (excluding controllers) associated with the action, inject any dependencies for them.
        /// </summary>
        /// <param name="filters"></param>
        protected virtual void InjectDependenciesIntoFilters(FilterInfo filters) {
            filters.ActionFilters
                .Where(filter => !filter.IsType<IController>())
                .ForEach(filter => ServiceLocator.Inject(filter));
            
            filters.AuthorizationFilters
                .Where(filter => !filter.IsType<IController>())                
                .ForEach(filter => ServiceLocator.Inject(filter));
            
            filters.ExceptionFilters
                .Where(filter => !filter.IsType<IController>())                
                .ForEach(filter => ServiceLocator.Inject(filter));

            filters.ResultFilters
                .Where(filter => !filter.IsType<IController>())
                .ForEach(filter => ServiceLocator.Inject(filter));
        }

        /// <summary>
        /// Gets the registered <see cref="IFilterFinder"/>.
        /// </summary>
        /// <returns></returns>
        protected virtual IFilterFinder GetFilterFinder() {
            if (filterFinder == null) {
                lock (_lock) {
                    if (filterFinder == null) {
                        try {
                            filterFinder = ServiceLocator.Resolve<IFilterFinder>();
                        } catch (ServiceResolutionException) {
                            filterFinder = new DefaultFilterFinder(ServiceLocator);
                        }
                    }
                }
            }

            return filterFinder;
        }
    }
}
