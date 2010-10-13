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
    using System.Web.Mvc;
    using ComponentModel;

    /// <summary>
    /// Defines a custom <see cref="ControllerActionInvoker"/> to use that will 
    /// infer actions that are not defined.
    /// </summary>
    public class TurbineActionInvoker : ControllerActionInvoker {
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

            return foundAction ?? new InferredActionDescriptor(actionName, controllerDescriptor);
        }
    }
}
